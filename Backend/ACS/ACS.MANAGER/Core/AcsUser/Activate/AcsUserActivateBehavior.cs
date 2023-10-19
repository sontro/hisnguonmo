using ACS.EFMODEL.DataModels;
using ACS.LibraryConfig;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.MANAGER.Core.AcsUser.Activate
{
    class AcsUserActivateBehaviorEv : BeanObjectBase, IAcsUserActivate
    {
        AcsUserActivateSDO entity;
        ACS_OTP otpRaw;

        internal AcsUserActivateBehaviorEv(CommonParam param, AcsUserActivateSDO data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsUserActivate.Run()
        {
            bool result = false;
            try
            {
                ACS_USER user = ValidInput() ? new AcsUserBO().Get<ACS_USER>(entity.LOGINNAME.ToLower()) : null;
                if (user != null)
                {
                    if (Valid(user))
                    {
                        user.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
                        string encryptPassword = new MOS.EncryptPassword.Cryptor().EncryptPassword(entity.PASSWORD, user.LOGINNAME);
                        user.PASSWORD = encryptPassword;
                        //user.ACTIVATE_CODE = "";
                        //user.ACTIVATE_EXPIRE_TIME = null;
                        if (DAOWorker.AcsUserDAO.Update(user))
                        {
                            otpRaw.IS_ACTIVE = 0;
                            if (!DAOWorker.AcsOtpDAO.Update(otpRaw))
                            {
                                Inventec.Common.Logging.LogSystem.Warn("Update IS_ACTIVE ve ngung hoat dong (= 0) vao bang acs_otp that bai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => otpRaw), otpRaw));
                                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_CapNhatThongTinTaiKhoanThatBai);
                            }
                            result = true;
                        }
                        else
                        {
                            Inventec.Common.Logging.LogSystem.Warn("Update du lieu user that bai. Doi mat khau that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => user), user));
                            ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__CapNhatThatBai);
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_CapNhatThongTinTaiKhoanThatBai);
                        }
                    }
                }
                else
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__ThieuThongTinBatBuoc);
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                    Inventec.Common.Logging.LogSystem.Warn("Kiem tra du lieu user khong hop le. Doi mat khau that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
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

        bool Valid(ACS_USER user)
        {
            bool valid = true;
            try
            {
                valid = valid && user != null;
                ACS.MANAGER.AcsOtp.AcsOtpFilterQuery otpFilterQuery = new MANAGER.AcsOtp.AcsOtpFilterQuery();
                otpFilterQuery.LOGINNAME__EXACT = user.LOGINNAME;
                otpFilterQuery.OTP_CODE__EXACT = entity.ACTIVATE_CODE;
                otpFilterQuery.OTP_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_OTP_TYPE.ID__ACTIVATE;
                otpFilterQuery.IS_ACTIVE = 1;
                otpFilterQuery.ORDER_FIELD = "EXPIRE_TIME";
                otpFilterQuery.ORDER_DIRECTION = "DESC";
                var rsotpRaw = valid ? new ACS.MANAGER.AcsOtp.AcsOtpManager().Get(otpFilterQuery) : null;
                otpRaw = (rsotpRaw != null && rsotpRaw.Data != null) ? rsotpRaw.Data.FirstOrDefault() : null;
                valid = valid && otpRaw != null;

                var dtExpire = valid ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(otpRaw.EXPIRE_TIME ?? 0) : null;
                valid = valid && dtExpire != null && dtExpire != DateTime.MinValue;
                valid = valid && otpRaw.OTP_CODE.Equals(entity.ACTIVATE_CODE);
                valid = valid && Inventec.Common.TypeConvert.Parse.ToInt64(dtExpire.Value.ToString("yyyyMMddHHmm")) >= Inventec.Common.TypeConvert.Parse.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm"));
            }
            catch (Exception ex)
            {
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__DuLieuTruyenLenKhongHopLe);
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        bool ValidInput()
        {
            bool valid = true;
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                if (String.IsNullOrEmpty(entity.ACTIVATE_CODE)) throw new ArgumentNullException("ACTIVATE_CODE");
                if (String.IsNullOrEmpty(entity.PASSWORD)) throw new ArgumentNullException("PASSWORD");
                valid = true;
            }
            catch (Exception ex)
            {
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
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
