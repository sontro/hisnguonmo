using ACS.EFMODEL.DataModels;
using ACS.LibraryConfig;
using ACS.MANAGER.AcsToken;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsUser;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using AutoMapper;
using Inventec.Core;
using Inventec.Token.AuthSystem;
using Inventec.Token.Core;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.Core.TokenSys.LoginBySecretKey
{
    class TokenSysLoginBySecretKeyBehaviorEv : BeanObjectBase, ITokenSysLoginBySecretKey
    {
        LoginBySecretKeySDO entity;
        string ApplicationCode = "HSK";
        ACS_TOKEN currentToken = null;
        ACS_USER userresult;

        internal TokenSysLoginBySecretKeyBehaviorEv(CommonParam param, LoginBySecretKeySDO data)
            : base(param)
        {
            entity = data;
        }

        Inventec.Token.Core.TokenData ITokenSysLoginBySecretKey.Run()
        {
            Inventec.Token.Core.TokenData result = null;           
            try
            {
                if (Check())
                {
                    userresult = new AcsUserBO().Get<ACS_USER>(entity.LOGIN_NAME.ToLower());
                    if (userresult != null)
                    {
                        bool valid = CheckUserIsActive(userresult);
                        if (valid)
                        {
                            AcsTokenAuthenticationSDO authenticationSDO = new AcsTokenAuthenticationSDO();
                            authenticationSDO = Mapper.Map<ACS_USER, AcsTokenAuthenticationSDO>(userresult);
                            authenticationSDO.ApplicationCode = this.ApplicationCode;
                            authenticationSDO.AppVersion = "";
                            AcsTokenBO tokenBO = new AcsTokenBO();
                            if (tokenBO.Authentication(authenticationSDO))
                            {
                                CommonParam commonParam = new Inventec.Core.CommonParam();
                                if (InsertTokenDataToDb(userresult, commonParam))
                                {
                                    userresult.PASSWORD = "";

                                    result = GenerateExtraToken(currentToken);

                                    //Ghi eventLog dang nhap
                                    AddActivityLog(currentToken);
                                    LogEventLoginBySecretKeySuccess();
                                }
                                else
                                {
                                    Inventec.Common.Logging.LogSystem.Warn("Call api TokenSysLoginBySecretKey Authentication thanh cong, tuy nhien InsertTokenDataToDb that bai. Dang nhap that bai. Du lieu dau vao____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                                }
                            }
                            else
                            {
                                CopyCommonParamInfo(tokenBO);
                                Inventec.Common.Logging.LogSystem.Warn("Call api TokenSysLoginBySecretKey that bai, Authentication that bai. Du lieu dau vao____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
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
                        Inventec.Common.Logging.LogSystem.Warn("Call api TokenSysLoginBySecretKey that bai, Khong tim thay user tuong ung voi loginname gui len____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                    }
                    return result;
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Warn("Call api TokenSysLoginBySecretKey that bai, du lieu nguoi dung gui len khong hop le____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
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
                AuthenticationCode = tokenData.AUTHENTICATION_CODE,
                RoleDatas = GetRoleData()
            };
        }

        private List<RoleData> GetRoleData()
        {
            List<RoleData> result = null;
            try
            {
                if (userresult != null)
                {
                    AcsTokenAuthenticationSDO authenticationSDO = new AcsTokenAuthenticationSDO();
                    authenticationSDO = Mapper.Map<ACS_USER, AcsTokenAuthenticationSDO>(userresult);
                    authenticationSDO.ApplicationCode = currentToken.APPLICATION_CODE;
                    authenticationSDO.AppVersion = currentToken.APP_VERSION;
                    var appRoleIds = new AuthorizeRoleForUsers(param, authenticationSDO).RunWithoutApplicationRole();
                    if (appRoleIds != null && appRoleIds.Count > 0)
                    {
                        ACS.MANAGER.Core.AcsRole.Get.AcsRoleFilterQuery filter = new ACS.MANAGER.Core.AcsRole.Get.AcsRoleFilterQuery();
                        filter.IDs = appRoleIds;
                        var roles = new ACS.MANAGER.Core.AcsRole.AcsRoleBO().Get<List<ACS_ROLE>>(filter);
                        if (roles != null && roles.Count > 0)
                        {
                            result = new List<RoleData>();
                            foreach (var role in roles)
                            {
                                result.Add(new RoleData() { RoleCode = role.ROLE_CODE, RoleName = role.ROLE_NAME });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public bool InsertTokenDataToDb(ACS_USER extraTokenData, CommonParam commonParam)
        {
            bool result = false;
            try
            {
                ACS.MANAGER.Token.TokenManager tokenManager = new ACS.MANAGER.Token.TokenManager();
                entity.LOGIN_ADDRESS = String.IsNullOrEmpty(entity.LOGIN_ADDRESS) ? Inventec.Token.ResourceSystem.ResourceTokenManager.GetRequestAddress() : entity.LOGIN_ADDRESS;
                if (!IsNotNull(extraTokenData)) throw new ArgumentNullException("extraTokenData is null");
                string MachineName = Environment.MachineName;
                ACS_TOKEN entityToken = new ACS_TOKEN();
                entityToken.APP_VERSION = "1.0.0.0";
                entityToken.APPLICATION_CODE = ApplicationCode;
                entityToken.EMAIL = extraTokenData.EMAIL;
                entityToken.LOGIN_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                entityToken.EXPIRE_TIME = Convert.ToInt64(DateTime.Now.AddMinutes(WebConfig.AuthSystem__TokenTimeout).ToString("yyyyMMddHHmmss"));
                entityToken.LAST_ACCESS_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                entityToken.LOGIN_ADDRESS = entity.LOGIN_ADDRESS;
                entityToken.LOGIN_NAME = extraTokenData.LOGINNAME;
                entityToken.MACHINE_NAME = MachineName;
                entityToken.MOBILE = extraTokenData.MOBILE;
                entityToken.USER_NAME = extraTokenData.USERNAME;

                //entityToken.TOKEN_CODE = ACS.UTILITY.GenerateToken.GenerateTokenCode(extraTokenData.LOGINNAME, entity.LOGIN_ADDRESS);
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

        private async Task AddActivityLog(ACS_TOKEN token)
        {
            try
            {
                ACS_ACTIVITY_LOG log = new ACS_ACTIVITY_LOG();
                log.ACTIVITY_TIME = Inventec.Common.DateTime.Get.Now().Value;
                log.ACTIVITY_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_ACTIVITY_TYPE.ID__LOGIN;
                log.EMAIL = token.EMAIL;
                log.EXECUTE_LOGINNAME = token.LOGIN_NAME;
                log.LOGINNAME = token.LOGIN_NAME;
                log.MOBILE = token.MOBILE;
                log.USERNAME = token.USER_NAME;
                log.APPLICATION_CODE = token.APPLICATION_CODE;
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

        async Task LogEventLoginBySecretKeySuccess()
        {
            try
            {
                CommonParam paramDNTC = new CommonParam();
                MessageUtil.SetMessage(paramDNTC, LibraryMessage.Message.Enum.Core_AcsUser_DangNhapThanhCong);
                SdaEventLogSDO eventLog = new SdaEventLogSDO();

                eventLog.Description = String.Format(paramDNTC.GetMessage(), this.ApplicationCode);
                eventLog.EventTime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                //if (!String.IsNullOrEmpty(entity.IP))
                //    eventLog.Ip = entity.IP;
                //else
                eventLog.Ip = Inventec.Token.ResourceSystem.ResourceTokenManager.GetRequestAddress();
                eventLog.LogginName = entity.LOGIN_NAME;
                eventLog.AppCode = this.ApplicationCode;
                eventLog.IsSuccess = true;

                Inventec.Core.ApiResultObject<bool> aro = await ACS.ApiConsumerManager.ApiConsumerStore.SdaConsumer.PostAsync<Inventec.Core.ApiResultObject<bool>>("/api/SdaEventLog/Create", new CommonParam(), eventLog);
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
                if (entity.LOGIN_NAME == null) throw new ArgumentNullException("LOGIN_NAME");
                if (entity.SECRET_KEY == null) throw new ArgumentNullException("SECRET_KEY");

                entity.LOGIN_NAME = entity.LOGIN_NAME.Trim().ToLower();
                if (!String.IsNullOrEmpty(entity.APP_CODE))
                    this.ApplicationCode = entity.APP_CODE;
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
                    Inventec.Common.Logging.LogSystem.Warn("Call api TokenSysLoginBySecretKey that bai____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
