using ACS.EFMODEL.DataModels;
using ACS.LibraryConfig;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.MANAGER.Core.AcsOtp.OtpVerify
{
    class AcsOtpVerifyBehaviorEv : BeanObjectBase, IAcsOtpVerify
    {
        OtpVerifySDO entity;

        internal AcsOtpVerifyBehaviorEv(CommonParam param, OtpVerifySDO data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsOtpVerify.Run()
        {
            bool result = false;
            try
            {
                if (!Valid(entity))
                {
                    Inventec.Common.Logging.LogSystem.Warn("Verify otp that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__CapNhatThatBai);
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsOtp_VerifyYeuCauCapOtpThatBai);
                }
                else
                {
                    result = true;
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

        bool Valid(OtpVerifySDO user)
        {
            bool valid = true;
            try
            {
                valid = valid && user != null;
                ACS.MANAGER.AcsOtp.AcsOtpFilterQuery otpFilterQuery = new MANAGER.AcsOtp.AcsOtpFilterQuery();
                otpFilterQuery.OTP_CODE__EXACT = entity.Otp;
                otpFilterQuery.MOBILE__EXACT = GetPrefixMobileNumber(entity.Mobile);
                //otpFilterQuery.OTP_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_OTP_TYPE.ID__OTHER;//TODO tạm bỏ đi
                otpFilterQuery.IS_ACTIVE = 1;
                otpFilterQuery.ORDER_FIELD = "EXPIRE_TIME";
                otpFilterQuery.ORDER_DIRECTION = "DESC";
                var rsotpRaw = valid ? new ACS.MANAGER.AcsOtp.AcsOtpManager().Get(otpFilterQuery) : null;
                var otpRaw = (rsotpRaw != null && rsotpRaw.Data != null) ? rsotpRaw.Data.FirstOrDefault() : null;
                valid = valid && otpRaw != null;

                var dtExpire = valid ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(otpRaw.EXPIRE_TIME ?? 0) : null;
                valid = valid && dtExpire != null && dtExpire != DateTime.MinValue;
                valid = valid && otpRaw.OTP_CODE.Equals(entity.Otp);
                valid = valid && Inventec.Common.TypeConvert.Parse.ToInt64(dtExpire.Value.ToString("yyyyMMddHHmm")) >= Inventec.Common.TypeConvert.Parse.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm"));
                Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => otpFilterQuery), otpFilterQuery)+Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => rsotpRaw), rsotpRaw));
            }
            catch (Exception ex)
            {
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

        bool ValidInput()
        {
            bool valid = true;
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                if (String.IsNullOrEmpty(entity.Mobile)) throw new ArgumentNullException("Mobile");
                if (String.IsNullOrEmpty(entity.Otp)) throw new ArgumentNullException("Otp");
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
    }
}
