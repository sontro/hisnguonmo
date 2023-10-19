using ACS.EFMODEL.DataModels;
using ACS.LibraryConfig;
using ACS.MANAGER.AcsOtp;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.MANAGER.Core.AcsUser.ChangPasswordWithOtp
{
    class AcsUserChangPasswordWithOtpBehaviorEv : BeanObjectBase, IAcsUserChangPasswordWithOtp
    {
        AcsUserChangePasswordWithOtpSDO entity;
        ACS_OTP optRawData;
        ACS_USER user;

        int timeout = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["ACS.AcsSystem.ResetPassword.Timeout"] ?? "0");

        internal AcsUserChangPasswordWithOtpBehaviorEv(CommonParam param, AcsUserChangePasswordWithOtpSDO data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsUserChangPasswordWithOtp.Run()
        {
            bool result = false;
            try
            {
                if (Valid())
                {
                    user = new ACS_USER();
                    CommonParam commonParam = new CommonParam();
                    if (AcsUserCheckVerifyValidDataForAuthorize.Verify(commonParam, ref user, optRawData.LOGINAME.ToLower()))
                    {
                        if (user != null)// && user.MOBILE == entity.Mobile
                        {
                            string encryptPassword = new MOS.EncryptPassword.Cryptor().EncryptPassword(entity.NewPassword, user.LOGINNAME.ToLower());
                            user.PASSWORD = encryptPassword;
                            optRawData.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__FALSE;
                            if (DAOWorker.AcsOtpDAO.Update(optRawData) && DAOWorker.AcsUserDAO.Update(user))
                            {
                                result = true;
                            }
                            else
                            {
                                user.PASSWORD = "";
                                Inventec.Common.Logging.LogSystem.Warn("Update du lieu user that bai. Doi mat khau that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => user), user) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => optRawData), optRawData));
                                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__CapNhatThatBai);
                            }
                        }
                        else
                        {
                            ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                            Inventec.Common.Logging.LogSystem.Warn("Kiem tra du lieu user khong hop le. Doi mat khau that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => optRawData), optRawData));
                        }
                    }
                    else
                    {
                        ACS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_MatKhauCuKhongChinhXac);
                    }
                }

                if (result) this.AddActivityLog(user);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Valid()
        {
            bool valid = false;
            try
            {
                valid = entity != null && !String.IsNullOrEmpty(entity.Mobile) && !String.IsNullOrEmpty(entity.Loginname) && !String.IsNullOrEmpty(entity.Otp) && !String.IsNullOrEmpty(entity.NewPassword);
                if (!valid)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__DuLieuTruyenLenKhongHopLe);
                    Inventec.Common.Logging.LogSystem.Warn(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => optRawData), optRawData));
                    return valid;
                }


                AcsOtpManager acsOtpManager = new AcsOtpManager();
                AcsOtpFilterQuery otpFilterQuery = new AcsOtpFilterQuery();
                otpFilterQuery.OTP_CODE__EXACT = entity.Otp;
                //otpFilterQuery.OTP_TYPE = StandardConstant.OTP_TYPE_CHANGEPASS;
                otpFilterQuery.OTP_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_OTP_TYPE.ID__CHANGE_PASS;
                otpFilterQuery.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
                otpFilterQuery.LOGINNAME__EXACT = entity.Loginname;
                optRawData = acsOtpManager.Get(otpFilterQuery).Data.FirstOrDefault();
                valid = valid && optRawData != null && optRawData.OTP_CODE == entity.Otp;
                if (!valid)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_ChuaThucHienGuiYeuCauGuiOtpDoiMatKhau);
                    Inventec.Common.Logging.LogSystem.Warn(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => optRawData), optRawData));
                    return valid;
                }
                user = new ACS_USER();
                if (!AcsUserCheckVerifyValidDataForAuthorize.Verify(param, ref user, entity.Loginname))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_GuiYeuCauOtpDoiMatKhauTaiKhoanTruyCapGuiLenKhongHopLe);
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                    Inventec.Common.Logging.LogSystem.Warn("Kiem tra du lieu user khong hop le. Doi mat khau that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => optRawData), optRawData));
                    return false;
                }
                valid = valid && GetPrefixMobileNumber(entity.Mobile) == GetPrefixMobileNumber(user.MOBILE);
                if (!valid)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_GuiYeuCauOtpDoiMatKhauSoDienThoaiGuiLenKhongKhopVoiSoDTDaDangKy);
                    Inventec.Common.Logging.LogSystem.Warn(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => optRawData), optRawData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_DaQuaThoiHanResetMatKhau);
                    return valid;
                }
                long timeCheck = Inventec.Common.TypeConvert.Parse.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss")) - (optRawData.EXPIRE_TIME ?? 0);
                if (optRawData.EXPIRE_TIME.HasValue && timeCheck > timeout)
                {
                    valid = false;
                    Inventec.Common.Logging.LogSystem.Warn("Phien reset mat khau da qua han. Du lieu dau vao: " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => optRawData), optRawData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_DaQuaThoiHanResetMatKhau);
                    return valid;
                }
                if (timeout == 0) timeout = 86400;
                else
                    timeout = timeout * 60;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => optRawData), optRawData));
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }

            return valid;
        }

        string GetPrefixMobileNumber(string mobile)
        {
            string prefixMobile = mobile;
            if (!String.IsNullOrEmpty(prefixMobile))
            {
                if (prefixMobile.StartsWith("0"))
                {
                    prefixMobile = String.Format("{0}{1}", "84", mobile.Substring(1));
                }
            }

            return prefixMobile;
        }

        private void AddActivityLog(ACS_USER user)
        {
            try
            {
                ACS_ACTIVITY_LOG log = new ACS_ACTIVITY_LOG();
                log.ACTIVITY_TIME = Inventec.Common.DateTime.Get.Now().Value;
                log.ACTIVITY_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_ACTIVITY_TYPE.ID__CHANGE_PASS;
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
