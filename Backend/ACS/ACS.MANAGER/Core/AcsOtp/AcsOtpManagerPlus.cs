using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using ACS.SDO;
using ACS.MANAGER.Core.Check;

namespace ACS.MANAGER.AcsOtp
{
    public partial class AcsOtpManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<ACS_OTP> GetByPhone(AcsOtpFilterQuery filter)
        {
            ApiResultObject<ACS_OTP> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                valid = valid && AcsOtpCheckVerifyValidData.Verify(param, filter);
                ACS_OTP resultData = null;
                if (valid)
                {
                    AcsOtpFilterQuery otpFilterQuery = new AcsOtpFilterQuery();
                    otpFilterQuery.MOBILE__EXACT = filter.MOBILE__EXACT;
                    otpFilterQuery.OTP_TYPE = (short)IMSys.DbConfig.ACS_RS.ACS_OTP_TYPE.ID__ACTIVATE;
                    otpFilterQuery.IS_HAS_EXPIRE = true;
                    var dataGet = new AcsOtpGet(param).Get(otpFilterQuery);
                    if (dataGet != null && dataGet.Count > 0)
                    {
                        resultData = new ACS_OTP();
                        resultData.OTP_CODE = dataGet.First().OTP_CODE;
                        resultData.EXPIRE_TIME = dataGet.First().EXPIRE_TIME;
                        resultData.EMAIL = dataGet.First().EMAIL;
                        resultData.LOGINAME = dataGet.First().LOGINAME;
                        resultData.USERNAME = dataGet.First().USERNAME;
                    }
                    else
                    {
                        param.Messages.Add(MessageUtil.GetMessage(LibraryMessage.Message.Enum.Core_AcsOtp_OtpDaHetHan));
                    }
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<bool> Required(OtpRequiredSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                bool isSuccess = false;
                if (valid)
                {
                    resultData = new AcsOtpRequired(param).Required(data);
                    isSuccess = true;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<OtpRequiredForLoginResultSDO> RequiredForLogin(OtpRequiredForLoginSDO data)
        {
            ApiResultObject<OtpRequiredForLoginResultSDO> result = new ApiResultObject<OtpRequiredForLoginResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                OtpRequiredForLoginResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    resultData = new AcsOtpRequiredForLogin(param).Required(data);
                    isSuccess = resultData != null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> RequiredWithMessage(OtpRequiredWithMessageSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                bool isSuccess = false;
                if (valid)
                {
                    resultData = new AcsOtpRequiredWithMessage(param).Required(data);
                    isSuccess = true;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> RequiredOnly(OtpRequiredOnlySDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                bool isSuccess = false;
                if (valid)
                {
                    resultData = new AcsOtpRequiredOnly(param).RequiredOnly(data);
                    isSuccess = true;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> RequiredOnlyWithMessage(OtpRequiredOnlyWithMessageSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                bool isSuccess = false;
                if (valid)
                {
                    resultData = new AcsOtpRequiredOnlyWithMessage(param).RequiredOnly(data);
                    isSuccess = true;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Verify(OtpVerifySDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                bool isSuccess = false;
                if (valid)
                {
                    resultData = new AcsOtpVerify(param).Verify(data);
                    isSuccess = true;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<Inventec.Token.Core.TokenData> VerifyForLogin(OtpVerifyForLoginSDO data)
        {
            ApiResultObject<Inventec.Token.Core.TokenData> result = new ApiResultObject<Inventec.Token.Core.TokenData>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                Inventec.Token.Core.TokenData resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    resultData = new AcsOtpVerifyForLogin(param).Verify(data);
                    isSuccess = resultData != null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<ACS_OTP>> CreateList(List<ACS_OTP> data)
        {
            ApiResultObject<List<ACS_OTP>> result = new ApiResultObject<List<ACS_OTP>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<ACS_OTP> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new AcsOtpCreate(param).CreateList(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<ACS_OTP>> UpdateList(List<ACS_OTP> data)
        {
            ApiResultObject<List<ACS_OTP>> result = new ApiResultObject<List<ACS_OTP>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<ACS_OTP> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new AcsOtpUpdate(param).UpdateList(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> DeleteList(List<long> ids)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    List<ACS_OTP> listOtp = new List<ACS_OTP>();
                    foreach (var item in ids)
                    {
                        ACS_OTP otp = new ACS_OTP();
                        otp.ID = item;
                        listOtp.Add(otp);
                    }

                    resultData = new AcsOtpTruncate(param).TruncateList(listOtp);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> RequiredOnlyWithTemplateCode(OtpRequiredOnlyWithTemplateCodeSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                bool isSuccess = false;
                if (valid)
                {
                    resultData = new AcsOtpRequiredOnlyWithTemplateCode(param).RequiredOnly(data);
                    isSuccess = true;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
