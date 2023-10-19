using ACS.EFMODEL.DataModels;
using ACS.LibraryConfig;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Common.Mail;
using Inventec.Core;
using System;
using System.Threading;

namespace ACS.MANAGER.Core.AcsUser.Create
{
    class AcsUserCreateBehaviorEv : BeanObjectBase, IAcsUserCreate
    {
        ACS_USER entity;

        internal AcsUserCreateBehaviorEv(CommonParam param, ACS_USER data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsUserCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check();
                if (result)
                {
                    string pass = ACS.UTILITY.Password.GeneratePassword();
                    if (LibraryConfig.WebConfig.IS_APPLICATION_GENERATE_PASSWORD)
                    {
                        pass = entity.LOGINNAME;
                    }
                    entity.PASSWORD = new MOS.EncryptPassword.Cryptor().EncryptPassword(pass, entity.LOGINNAME);

                    result = DAOWorker.AcsUserDAO.Create(entity);
                    entity.PASSWORD = "";
                    if (!result)
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThemMoiThatBai);
                        Inventec.Common.Logging.LogSystem.Warn("Tao moi nguoi dung that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                    }
                    else
                    {
                        CreateThreadSendMail(new MailSDO() { LoginName = entity.LOGINNAME, MailAddress = entity.EMAIL, Password = pass });
                    }
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
                    mail.Body = String.Format(WebConfig.MailServerGmail__Body, mailData.LoginName, mailData.Password, "<br/>");
                    //mail.Body = "Hệ thống xin thông báo mật khẩu mới của tài khoản " + mailData.LoginName + " là: " + mailData.Password + "<br/>Vui lòng đăng nhập hệ thống và thực hiện đổi mật khẩu để đảm bảo an toàn thông tin.";
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

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && AcsUserCheckVerifyValidData.Verify(param, entity);
                result = result && AcsUserCheckVerifyExistsCode.Verify(param, entity.LOGINNAME, null);
                entity.LOGINNAME = entity.LOGINNAME.ToLower();
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
