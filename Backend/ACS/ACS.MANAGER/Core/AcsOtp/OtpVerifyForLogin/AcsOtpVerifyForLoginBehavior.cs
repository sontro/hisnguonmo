using ACS.EFMODEL.DataModels;
using ACS.LibraryConfig;
using ACS.MANAGER.AcsToken;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.Core.TokenSys;
using ACS.MANAGER.Token;
using ACS.SDO;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using Inventec.Token.AuthSystem;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ACS.MANAGER.Core.AcsOtp.OtpVerifyForLogin
{
    class AcsOtpVerifyForLoginBehaviorEv : BeanObjectBase, IAcsOtpVerifyForLogin
    {
        OtpVerifyForLoginSDO entity;
        ACS_TOKEN currentToken = null;

        internal AcsOtpVerifyForLoginBehaviorEv(CommonParam param, OtpVerifyForLoginSDO data)
            : base(param)
        {
            entity = data;
        }

        Inventec.Token.Core.TokenData IAcsOtpVerifyForLogin.Run()
        {
            Inventec.Token.Core.TokenData result = null;
            try
            {
                bool valid = false;
                valid = Valid(entity);
                if (!valid)
                {
                    Inventec.Common.Logging.LogSystem.Warn("Verify otp for login that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__CapNhatThatBai);
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsOtp_VerifyYeuCauCapOtpThatBai);
                }

                AcsTokenBO acsTokenBO = new AcsTokenBO();
                AcsTokenLoginSDO acsTokenLoginSDO = new AcsTokenLoginSDO();
                acsTokenLoginSDO.APPLICATION_CODE = entity.ApplicationCode;
                acsTokenLoginSDO.LOGIN_NAME = entity.LoginName;
                acsTokenLoginSDO.PASSWORD = entity.Password;

                ACS_USER user = acsTokenBO.Login(acsTokenLoginSDO);
                if (user != null)
                {
                    if (valid)
                    {
                        CommonParam commonParam = new Inventec.Core.CommonParam();
                        if (InsertTokenDataToDb(user, commonParam))
                        {
                            user.PASSWORD = "";
                            result = GenerateExtraToken(currentToken);
                        }
                        else
                        {
                            entity.Password = "";
                            Inventec.Common.Logging.LogSystem.Warn("Call api AcsOtp/OtpVerifyForLogin Login thanh cong, tuy nhien InsertTokenDataToDb that bai. Dang nhap that bai. Du lieu dau vao____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                        }
                    }
                }
                else
                {
                    CopyCommonParamInfo(acsTokenBO);
                    entity.Password = "";
                    Inventec.Common.Logging.LogSystem.Warn("Call api AcsOtp/OtpVerifyForLogin that bai, Login that bai. Du lieu dau vao____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
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

        Inventec.Token.Core.TokenData GenerateExtraToken(ACS_TOKEN tokenData)
        {
            return new Inventec.Token.Core.TokenData()
            {
                ExpireTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(tokenData.EXPIRE_TIME).Value,
                LastAccessTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(tokenData.LAST_ACCESS_TIME ?? 0).Value,
                LoginAddress = tokenData.LOGIN_ADDRESS,
                LoginTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(tokenData.LOGIN_TIME).Value,
                MachineName = tokenData.MACHINE_NAME,
                RenewCode = tokenData.RENEW_CODE,
                TokenCode = tokenData.TOKEN_CODE,
                User = new Inventec.Token.Core.UserData()
                {
                    ApplicationCode = tokenData.APPLICATION_CODE,
                    Email = tokenData.EMAIL,
                    LoginName = tokenData.LOGIN_NAME,
                    Mobile = tokenData.MOBILE,
                    UserName = tokenData.USER_NAME
                },
                VersionApp = tokenData.APP_VERSION,
                AuthorSystemCode = tokenData.AUTHOR_SYSTEM_CODE,
                AuthenticationCode = tokenData.AUTHENTICATION_CODE
            };
        }

        private bool InsertTokenDataToDb(ACS_USER extraTokenData, CommonParam commonParam)
        {
            bool result = false;
            try
            {
                ACS.MANAGER.Token.TokenManager tokenManager = new ACS.MANAGER.Token.TokenManager();

                entity.LoginAddress = String.IsNullOrEmpty(entity.LoginAddress) ? Inventec.Token.ResourceSystem.ResourceTokenManager.GetRequestAddress() : entity.LoginAddress;
                if (!IsNotNull(extraTokenData)) throw new ArgumentNullException("extraTokenData is null");
                string MachineName = Environment.MachineName;
                ACS_TOKEN entityToken = new ACS_TOKEN();
                entityToken.APP_VERSION = "1.0.0.0";
                entityToken.APPLICATION_CODE = entity.ApplicationCode;
                entityToken.EMAIL = extraTokenData.EMAIL;
                entityToken.LOGIN_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                entityToken.EXPIRE_TIME = Convert.ToInt64(DateTime.Now.AddMinutes(WebConfig.AuthSystem__TokenTimeout).ToString("yyyyMMddHHmmss"));
                entityToken.LAST_ACCESS_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                entityToken.LOGIN_ADDRESS = entity.LoginAddress;
                entityToken.LOGIN_NAME = extraTokenData.LOGINNAME;
                entityToken.USER_NAME = extraTokenData.USERNAME;
                entityToken.MACHINE_NAME = MachineName;
                entityToken.MOBILE = extraTokenData.MOBILE;
                //entityToken.TOKEN_CODE = ACS.UTILITY.GenerateToken.GenerateTokenCode(extraTokenData.LOGINNAME, entity.LoginAddress);
                //entityToken.RENEW_CODE = ACS.UTILITY.GenerateToken.GenerateRenewCode(entityToken.TOKEN_CODE);
                entityToken.TOKEN_CODE = tokenManager.GenerateTokenCode(new ExtraTokenData()
                {
                    ExpireTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(entityToken.EXPIRE_TIME).Value,
                    LastAccessTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(entityToken.LAST_ACCESS_TIME ?? 0).Value,
                    LoginAddress = entityToken.LOGIN_ADDRESS,
                    LoginTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(entityToken.LOGIN_TIME).Value,
                    MachineName = entityToken.MACHINE_NAME,
                    ValidAddress = entityToken.LOGIN_ADDRESS,
                    VersionApp = entityToken.APP_VERSION,
                    AuthorSystemCode = entityToken.AUTHOR_SYSTEM_CODE,
                    AuthenticationCode = entityToken.AUTHENTICATION_CODE,
                    User = new Inventec.Token.Core.UserData()
                    {
                        ApplicationCode = entityToken.APPLICATION_CODE,
                        Email = entityToken.EMAIL,
                        GCode = entityToken.GROUP_CODE,
                        LoginName = entityToken.LOGIN_NAME,
                        Mobile = entityToken.MOBILE,
                        UserName = entityToken.USER_NAME
                    }
                });
                entityToken.RENEW_CODE = tokenManager.GenerateRenewCode(entityToken.TOKEN_CODE);

                commonParam = new CommonParam();
                var rs = new AcsTokenManager(commonParam).Create(entityToken);
                currentToken = (rs != null ? rs.Data : null);
                if (currentToken != null)
                {
                    Inventec.Token.AuthSystem.ExtraTokenData tokenData = tokenManager.GenerateExtraToken(currentToken);
                    result = tokenManager.InsertTokenInRam(tokenData);
                    tokenManager.InitThreadSyncTokenInsert(tokenData);
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("InsertTokenDataToDb that bai. "
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => extraTokenData), extraTokenData));
                }
            }
            catch (ArgumentNullException ex)
            {
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            return result;
        }

        bool Valid(OtpVerifyForLoginSDO user)
        {
            bool valid = true;
            try
            {
                valid = valid && user != null;
                ACS.MANAGER.AcsOtp.AcsOtpFilterQuery otpFilterQuery = new MANAGER.AcsOtp.AcsOtpFilterQuery();
                otpFilterQuery.OTP_CODE__EXACT = entity.Otp;
                otpFilterQuery.LOGINNAME__EXACT = entity.LoginName;
                otpFilterQuery.OTP_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_OTP_TYPE.ID__VERIFY_FOR_LOGIN;
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
            }
            catch (Exception ex)
            {
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
                if (String.IsNullOrEmpty(entity.LoginName)) throw new ArgumentNullException("LoginName");
                if (String.IsNullOrEmpty(entity.Password)) throw new ArgumentNullException("Password");
                if (String.IsNullOrEmpty(entity.ApplicationCode)) throw new ArgumentNullException("ApplicationCode");
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
