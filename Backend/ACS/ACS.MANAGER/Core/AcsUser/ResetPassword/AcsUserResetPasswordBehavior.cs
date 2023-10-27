using ACS.EFMODEL.DataModels;
using ACS.LibraryConfig;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Common.Mail;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser.ResetPassword
{
    class AcsUserResetPasswordBehaviorEv : BeanObjectBase, IAcsUserResetPassword
    {
        ACS_USER entity;

        internal AcsUserResetPasswordBehaviorEv(CommonParam param, ACS_USER data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsUserResetPassword.Run()
        {
            bool result = false;
            try
            {
                ACS_USER user = new AcsUserBO().Get<ACS_USER>(entity.LOGINNAME.Trim().ToLower());
                if (user == null)
                {
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                    Inventec.Common.Logging.LogSystem.Warn("Kiem tra du lieu user khong hop le. Doi mat khau that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                }
                else
                {
                    if (user != null)
                    {
                        user.LOGINNAME = user.LOGINNAME.Trim().ToLower();
                        string pass = ACS.UTILITY.Password.GeneratePassword();
                        if (LibraryConfig.WebConfig.IS_APPLICATION_GENERATE_PASSWORD)
                        {
                            pass = user.LOGINNAME;
                        }
                        user.PASSWORD = new MOS.EncryptPassword.Cryptor().EncryptPassword(pass, user.LOGINNAME);
                        result = DAOWorker.AcsUserDAO.Update(user);
                        if (!result)
                        {
                            ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__CapNhatThatBai);
                            Inventec.Common.Logging.LogSystem.Warn("Reset mat khau cua nguoi dung that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => user), user));
                        }
                        else
                        {
                            try
                            {
                                //Send mail information username/password to email of user
                                Mail mail = new Mail();
                                MailServerGmail serverGmail = new MailServerGmail();
                                serverGmail.User = WebConfig.MailServerGmail__User;
                                serverGmail.Password = WebConfig.MailServerGmail__Password;
                                if (!String.IsNullOrEmpty(entity.EMAIL))
                                {
                                    mail.SetTo(entity.EMAIL);
                                   // mail.Body = "Hệ thống xin thông báo mật khẩu mới của tài khoản " + entity.LOGINNAME + " là: " + pass + "<br/>Vui lòng đăng nhập hệ thống và thực hiện đổi mật khẩu để đảm bảo an toàn thông tin.";
                                    mail.Body = String.Format(WebConfig.MailServerGmail__Body, entity.LOGINNAME, pass, "<br/>");
                                    mail.FromEmail = WebConfig.MailServerGmail__User;
                                    mail.FromName = "IMSys";
                                    mail.IsBodyHtml = true;
                                    mail.Subject = "IMSys - Thông tin tài khoản";
                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                    if (!mail.SendMail(serverGmail))
                                    {
                                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsUser_GuiEmailXacNhanThongTinTaiKhoanThatBai);
                                        Inventec.Common.Logging.LogSystem.Warn("Doi mat khau tai khoan " + entity.LOGINNAME + " thanh cong, tuy nhien khong gui duoc email xac nhan thong tin tai khoan den nguoi dung . " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Inventec.Common.Logging.LogSystem.Warn(ex);
                                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsUser_GuiEmailXacNhanThongTinTaiKhoanThatBai);
                            }
                        }
                    }
                    else
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                        Inventec.Common.Logging.LogSystem.Warn("Kiem tra tai khoan nguoi dung (ten dang nhap, mat khau) khong hop le. Doi mat khau that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                    }

                    if (result) this.AddActivityLog(user);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
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
                log.ACTIVITY_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_ACTIVITY_TYPE.ID__RESET_PASS;
                log.EMAIL = user.EMAIL;
                log.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
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
