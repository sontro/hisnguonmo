using ACS.EFMODEL.DataModels;
using ACS.LibraryConfig;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using Inventec.Common.Mail;
using Inventec.Core;
using System;
using System.Text;

namespace ACS.MANAGER.Core.AcsUser.ResetPasswordB
{
    class AcsUserResetPasswordBBehaviorEv : BeanObjectBase, IAcsUserResetPasswordB
    {
        AcsUserResetPasswordTDO entity;
        string URI_API_ACS = System.Configuration.ConfigurationSettings.AppSettings["Inventec.Token.Base.ApiConsumerStore.AcsConsumer.Uri"];

        internal AcsUserResetPasswordBBehaviorEv(CommonParam param, AcsUserResetPasswordTDO data)
            : base(param)
        {
            entity = data;
        }

        AcsCheckResetPasswordResultTDO IAcsUserResetPasswordB.Run()
        {
            AcsCheckResetPasswordResultTDO result = null;
            bool valid = true;
            try
            {
                ACS_USER user = new AcsUserBO().Get<ACS_USER>(entity.LoginName.Trim().ToLower());
                if (user == null)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TenDangNhapKhongChinhXac);
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                    Inventec.Common.Logging.LogSystem.Warn("Kiem tra du lieu user khong hop le. Doi mat khau that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                }
                else
                {
                    string emailSendLink = String.Empty;
                    if (!String.IsNullOrEmpty(user.EMAIL))
                        emailSendLink = user.EMAIL;

                    if (String.IsNullOrEmpty(emailSendLink))
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_EmailKhongHopLe);
                        Inventec.Common.Logging.LogSystem.Warn("Email gui link reset mat khau khong hop le. Du lieu dau vao: " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + "__Ket qua tra ve: " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + ", emailSendLink = " + emailSendLink);
                        valid = false;
                    }

                    if (valid)
                    {
                        try
                        {
                            string paramBase64Key = ACS.UTILITY.Util.Base64Encode(user.LOGINNAME + ":" + user.EMAIL + ":" + DateTime.Now.ToString("yyyyMMddHHmmss"));

                            var body = new StringBuilder();
                            body.AppendFormat("Xin chào, {0}\n", user.USERNAME);
                            body.AppendLine(@"Yêu cầu reset mật khẩu của bạn đã được hệ thống xử lý, để hoàn tất việc này, bạn vui lòng click vào link xác nhận bên dưới.");
                            body.AppendLine("<a href=\"" + URI_API_ACS + "/AcsSystem/ResetPassword/" + paramBase64Key + "\">Xác nhận reset mật khẩu</a>");

                            //Send mail information username/password to email of user
                            Mail mail = new Mail();
                            MailServerGmail serverGmail = new MailServerGmail();
                            serverGmail.User = WebConfig.MailServerGmail__User;
                            serverGmail.Password = WebConfig.MailServerGmail__Password;
                            mail.SetTo(emailSendLink);
                            mail.Body = body.ToString();
                            mail.FromEmail = WebConfig.MailServerGmail__User;
                            mail.FromName = "IMSys";
                            mail.IsBodyHtml = true;
                            mail.Subject = "IMSys - Xác nhận reset mật khẩu";
                            mail.SubjectEncoding = System.Text.Encoding.UTF8;
                            if (!mail.SendMail(serverGmail))
                            {
                                param.Messages.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.Core_AcsUser_GuiEmailXacNhanResetMatKhauDenNguoiDungThatBai), emailSendLink));
                                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsUser_GuiEmailXacNhanThongTinTaiKhoanThatBai);
                                Inventec.Common.Logging.LogSystem.Warn("Gui email xac nhan thong tin doi mat khau den email " + emailSendLink + " that bai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                            }
                            else
                            {
                                result = new AcsCheckResetPasswordResultTDO();
                                //result.UserName = user.USERNAME;
                                //result.Phone = user.MOBILE;
                                result.Email = user.EMAIL;
                            }
                        }
                        catch (Exception ex)
                        {
                            Inventec.Common.Logging.LogSystem.Warn(ex);
                            param.Messages.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.Core_AcsUser_GuiEmailXacNhanResetMatKhauDenNguoiDungThatBai), emailSendLink));
                            ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsUser_GuiEmailXacNhanThongTinTaiKhoanThatBai);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
