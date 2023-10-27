using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using AutoMapper;
using Inventec.Core;
using SDA.SDO;
using System;

namespace ACS.MANAGER.Core.TokenSys.Login
{
    class TokenSysLoginBehaviorEv : BeanObjectBase, ITokenSysLogin
    {
        AcsTokenLoginSDO entity;

        internal TokenSysLoginBehaviorEv(CommonParam param, AcsTokenLoginSDO data)
            : base(param)
        {
            entity = data;
        }

        ACS_USER ITokenSysLogin.Run()
        {
            ACS_USER result = null;
            try
            {
                if (Check())
                {
                    bool valid = (AcsUserCheckVerifyValidDataForLogin.Verify(param, ref result, entity.LOGIN_NAME, entity.PASSWORD));
                    if (valid)
                    {
                        valid = valid && CheckUserIsActive(result);
                        if (valid)
                        {
                            AcsTokenAuthenticationSDO authenticationSDO = new AcsTokenAuthenticationSDO();
                            authenticationSDO = Mapper.Map<ACS_USER, AcsTokenAuthenticationSDO>(result);
                            authenticationSDO.ApplicationCode = entity.APPLICATION_CODE;
                            authenticationSDO.AppVersion = entity.APP_VERSION;
                            AcsTokenBO tokenBO = new AcsTokenBO();
                            if (tokenBO.Authentication(authenticationSDO))
                            {
                                result.PASSWORD = "";

                                //Ghi eventLog dang nhap
                                LogEventLoginSuccess();
                                AddActivityLog(result);
                            }
                            else
                            {
                                CopyCommonParamInfo(tokenBO);
                                result = null;
                            }
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    else
                    {
                        result = null;
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }

        void LogEventLoginSuccess()
        {
            try
            {
                CommonParam paramDNTC = new CommonParam();
                MessageUtil.SetMessage(paramDNTC, LibraryMessage.Message.Enum.Core_AcsUser_DangNhapThanhCong);
                SdaEventLogSDO eventLog = new SdaEventLogSDO();

                eventLog.Description = String.Format(paramDNTC.GetMessage(), entity.APPLICATION_CODE);
                eventLog.EventTime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                //if (!String.IsNullOrEmpty(entity.IP))
                //    eventLog.Ip = entity.IP;
                //else
                eventLog.Ip = Inventec.Token.ResourceSystem.ResourceTokenManager.GetRequestAddress();
                eventLog.LogginName = entity.LOGIN_NAME;
                eventLog.AppCode = entity.APPLICATION_CODE;
                eventLog.IsSuccess = true;

                Inventec.Core.ApiResultObject<bool> aro = ACS.ApiConsumerManager.ApiConsumerStore.SdaConsumer.Post<Inventec.Core.ApiResultObject<bool>>("/api/SdaEventLog/Create", new CommonParam(), eventLog);
                if (!(aro != null && aro.Success))
                {
                    Inventec.Common.Logging.LogSystem.Warn("Ghi log dang nhap he thong khong thanh cong. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => eventLog), eventLog));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        bool Check()
        {
            bool result = true;
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");

                entity.LOGIN_NAME = entity.LOGIN_NAME.Trim().ToLower();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Ham kiem tra tai khoan co bi tam khoa hay khong
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckUserIsActive(ACS_USER data)
        {
            bool valid = false;
            try
            {

                valid = (data != null && data.ID > 0 && data.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__TRUE);
                if (!valid && data != null)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TaiKhoanDangBiTamKhoa);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }


        private void AddActivityLog(ACS_USER user)
        {
            try
            {
                ACS_ACTIVITY_LOG log = new ACS_ACTIVITY_LOG();
                log.ACTIVITY_TIME = Inventec.Common.DateTime.Get.Now().Value;
                log.ACTIVITY_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_ACTIVITY_TYPE.ID__LOGIN;
                log.EMAIL = user.EMAIL;
                log.EXECUTE_LOGINNAME = user.LOGINNAME;
                log.LOGINNAME = user.LOGINNAME;
                log.MOBILE = user.MOBILE;
                log.USERNAME = user.USERNAME;
                log.APPLICATION_CODE = entity.APPLICATION_CODE;
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
