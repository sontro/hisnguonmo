using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsApplication;
using ACS.MANAGER.Core.AcsApplication.Get;
using ACS.MANAGER.Core.AcsApplicationRole;
using ACS.MANAGER.Core.AcsApplicationRole.Get;
using ACS.MANAGER.Core.AcsRole;
using ACS.MANAGER.Core.AcsRoleUser;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using Inventec.Common.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ACS.LibraryConfig;

namespace ACS.MANAGER.Core.AcsUser.Create
{
    class AcsUserCreateBehaviorSdo : BeanObjectBase, IAcsUserCreate
    {
        ACS.SDO.CreateAndGrantUserSDO entity;
        ACS_USER raw;
        ACS_APPLICATION currentApplication;
        ACS_APPLICATION_ROLE currentApplicationRole;
        ACS_ROLE currentRole;
        ACS_ROLE_USER currentRoleUser;

        internal AcsUserCreateBehaviorSdo(CommonParam param, ACS.SDO.CreateAndGrantUserSDO data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsUserCreate.Run()
        {
            bool result = false;
            try
            {
                raw = new ACS_USER();
                raw.LOGINNAME = entity.LoginName.ToLower();
                raw.MOBILE = entity.Mobile;
                raw.EMAIL = entity.Email;
                raw.USERNAME = entity.UserName;
                raw.APP_CREATOR = entity.AppCode;
                raw.APP_MODIFIER = raw.APP_CREATOR;
                string pass = ACS.UTILITY.Password.GeneratePassword();
                if (!String.IsNullOrEmpty(entity.Password))
                {
                    pass = entity.Password;
                }
                else if (LibraryConfig.WebConfig.IS_APPLICATION_GENERATE_PASSWORD)
                {
                    pass = raw.LOGINNAME;
                }

                raw.PASSWORD = new MOS.EncryptPassword.Cryptor().EncryptPassword(pass, raw.LOGINNAME);
                if (Check())
                {
                    raw.IS_ACTIVE = (entity.IsActive.HasValue && entity.IsActive.Value) ? IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE : IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__FALSE;
                    if (DAOWorker.AcsUserDAO.Create(raw))
                    {
                        currentRole = GetRoleByCode(entity.RoleCode);
                        if (currentRole == null)
                        {
                            ACS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TaoTaiKhoanHeThongTichHopThanhCongTuyNhienKhongGanDuocUserRoleDoRoleCodeGuiLenKhongHopLe);
                            Inventec.Common.Logging.LogSystem.Warn("Get Role by code not found. RoleCode=" + entity.RoleCode + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                        }
                        else
                        {
                            //tao moi role user
                            result = CreateRoleUser(currentRole.ID);
                            raw.PASSWORD = "";
                            if (!result)
                            {
                                ACS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TaoTaiKhoanHeThongTichHopThanhCongTuyNhienKhongGanDuocUserRole);
                                Inventec.Common.Logging.LogSystem.Warn("CreateRoleUser fail. He thong se tu dong rollback role & user da tao truoc do." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                                return false;
                            }
                            if (result)
                            {
                                CreateThreadSendMail(new MailSDO() { LoginName = raw.LOGINNAME, MailAddress = raw.EMAIL, Password = pass });
                            }
                        }
                    }
                    else
                    {
                        result = false;
                        ACS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TaoTaiKhoanThatBai);
                        Inventec.Common.Logging.LogSystem.Warn("CreateUser fail.");
                    }
                }
                if (result && raw.IS_ACTIVE == IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE) this.AddActivityLog(raw);
            }
            catch (Exception ex)
            {
                RollBackUser();
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void CreateThreadSendMail(object param)
        {
            Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(SendMailNewThread));
            thread.Priority = ThreadPriority.Normal;
            try
            {
                thread.Start(param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                thread.Abort();
            }
        }

        private void SendMailNewThread(object param)
        {
            try
            {
                this.SendMailProcess(param as MailSDO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        void SendMailProcess(MailSDO mailData)
        {
            try
            {
                //Send mail information username/password to email of user
                Mail mail = new Mail();
                MailServerGmail serverGmail = new MailServerGmail();
                serverGmail.User = WebConfig.MailServerGmail__User;
                serverGmail.Password = WebConfig.MailServerGmail__Password;
                if (!String.IsNullOrEmpty(mailData.MailAddress))
                {
                    mail.SetTo(mailData.MailAddress);
                    //mail.Body = "Hệ thống xin thông báo mật khẩu mới của tài khoản " + mailData.LoginName + " là: " + mailData.Password + "<br/>Vui lòng đăng nhập hệ thống và thực hiện đổi mật khẩu để đảm bảo an toàn thông tin.";
                    mail.Body = String.Format(WebConfig.MailServerGmail__Body, mailData.LoginName, mailData.Password, "<br/>");
                    mail.FromEmail = WebConfig.MailServerGmail__User;
                    mail.FromName = "IMSys";
                    mail.IsBodyHtml = true;
                    mail.Subject = "IMSys - Thông tin tài khoản";
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    if (!mail.SendMail(serverGmail))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsUser_GuiEmailXacNhanThongTinTaiKhoanThatBai);
                        mailData.Password = "";
                        Inventec.Common.Logging.LogSystem.Warn("Tao tai khoan " + mailData.LoginName + " thanh cong, tuy nhien khong gui duoc email xac nhan thong tin tai khoan den nguoi dung . " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => mailData), mailData));
                    }
                    else
                    {
                        Inventec.Common.Logging.LogSystem.Debug("He thong da gui mail thong tin tai khoan den email dang ky cua nguoi dung. Chi tiet: " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => mailData.LoginName), mailData.LoginName) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => mailData.MailAddress), mailData.MailAddress));
                    }
                }
            }
            catch (Exception ex)
            {
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsUser_GuiEmailXacNhanThongTinTaiKhoanThatBai);
                Inventec.Common.Logging.LogSystem.Warn(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param), ex);
            }
        }

        void RollBackUser()
        {
            try
            {
                DAOWorker.AcsUserDAO.Truncate(raw);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        void RollBackRoleUser()
        {
            try
            {
                DAOWorker.AcsRoleUserDAO.Truncate(currentRoleUser);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        void RollBackRole()
        {
            try
            {
                DAOWorker.AcsRoleDAO.Truncate(currentRole);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        ACS_APPLICATION_ROLE GetAppRoleByAppCode(long applicationId)
        {
            ACS_APPLICATION_ROLE result = null;
            try
            {
                AcsApplicationRoleFilterQuery applicationRoleFilterQuery = new AcsApplicationRoleFilterQuery();
                applicationRoleFilterQuery.APPLICATION_ID = applicationId;
                result = new AcsApplicationRoleBO().Get<List<ACS_APPLICATION_ROLE>>(applicationRoleFilterQuery).FirstOrDefault();

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        bool CreateRoleUser(long roleId)
        {
            bool result = false;
            try
            {
                currentRoleUser = new ACS_ROLE_USER();
                currentRoleUser.ROLE_ID = roleId;
                currentRoleUser.USER_ID = raw.ID;
                result = new AcsRoleUserBO().Create(currentRoleUser);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        bool CreateApplicationRole(long roleId)
        {
            bool result = false;
            try
            {
                currentApplicationRole = new ACS_APPLICATION_ROLE();
                currentApplicationRole.ROLE_ID = roleId;
                currentApplicationRole.APPLICATION_ID = currentApplication.ID;

                result = new AcsApplicationRoleBO().Create(currentApplicationRole);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        bool CreateRole(ACS_ROLE role)
        {
            bool result = false;
            try
            {
                result = new AcsRoleBO().Create(role);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        ACS_ROLE GetRoleByCode(string roleCode)
        {
            ACS_ROLE result = null;
            try
            {
                result = new AcsRoleBO().Get<ACS_ROLE>(roleCode);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && AcsUserCheckVerifyValidData.Verify(param, entity);
                result = result && AcsUserCheckVerifyValidData.Verify(param, raw);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void AddActivityLog(ACS_USER user)
        {
            try
            {
                ACS_ACTIVITY_LOG log = new ACS_ACTIVITY_LOG();
                log.ACTIVITY_TIME = Inventec.Common.DateTime.Get.Now().Value;
                log.ACTIVITY_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_ACTIVITY_TYPE.ID__ACTIVATE;
                log.EMAIL = user.EMAIL;
                log.EXECUTE_LOGINNAME = user.LOGINNAME;
                log.LOGINNAME = user.LOGINNAME;
                log.MOBILE = user.MOBILE;
                log.USERNAME = user.USERNAME;
                log.APPLICATION_CODE = Inventec.Token.ResourceSystem.ResourceTokenManager.GetApplicationCode();
                try
                {
                    log.IP_ADDRESS = Inventec.Token.ResourceSystem.ResourceTokenManager.GetRequestAddress();
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }
                ActivityProcessor.ActivityLogCache.Push(log);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
