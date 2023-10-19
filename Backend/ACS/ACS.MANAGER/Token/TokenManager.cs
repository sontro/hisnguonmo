using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Manager;
using ACS.SDO;
using Inventec.Core;
using Inventec.Token.AuthSystem;
using Inventec.Token.Core;
using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Linq;
using ACS.LibraryConfig;
using SDA.SDO;
using System.Threading.Tasks;
using ACS.MANAGER.Core.Check;
using AutoMapper;
using ACS.MANAGER.Core.TokenSys;
using ACS.MANAGER.Core.AcsRole.Get;
using ACS.MANAGER.Core.AcsRole;

namespace ACS.MANAGER.Token
{
    /// <summary>
    /// Khong cho phep thua ke
    /// </summary>
    public sealed partial class TokenManager : Inventec.Backend.MANAGER.BusinessBase
    {
        AuthTokenManager authManager;
        const string MACHINE_NAME_PARAM = "MachineName";


        //public TokenManager()
        //    : base()
        //{
        //    authManager = new AuthTokenManager(GetValidUserData, UpdateUserPasswordToDb, IsGrantedUser, GetCredentialDataFromDb, InsertCredentialDataToDb, DeleteCredentialDataFromDb, DeleteAllCredentialDataFromDb, InsertTokenDataToDb, RemoveTokenDataFromDb, GetTokenDataByTokenCodeFromDb, GetTokenDataByRenewCodeFromDb, GetTokenDataByMachineNameFromDb);
        //}
        //public TokenManager(CommonParam param)
        //    : base(param)
        //{
        //    authManager = new AuthTokenManager(GetValidUserData, UpdateUserPasswordToDb, IsGrantedUser, GetCredentialDataFromDb, InsertCredentialDataToDb, DeleteCredentialDataFromDb, DeleteAllCredentialDataFromDb, InsertTokenDataToDb, RemoveTokenDataFromDb, GetTokenDataByTokenCodeFromDb, GetTokenDataByRenewCodeFromDb, GetTokenDataByMachineNameFromDb);
        //}

        public TokenManager()
            : base()
        {
            authManager = new AuthTokenManager(GetValidUserData, UpdateUserPasswordToDb, IsGrantedUser, GetCredentialDataFromDb, InsertCredentialDataToDb, DeleteCredentialDataFromDb, DeleteAllCredentialDataFromDb, InsertTokenDataToDb, RemoveTokenDataFromDb, GetTokenDataByTokenCodeFromDb, GetTokenDataByRenewCodeFromDb, RemoveOtherTokenDataByLoginNameFromDb, null);
        }
        public TokenManager(CommonParam param)
            : base(param)
        {
            authManager = new AuthTokenManager(GetValidUserData, UpdateUserPasswordToDb, IsGrantedUser, GetCredentialDataFromDb, InsertCredentialDataToDb, DeleteCredentialDataFromDb, DeleteAllCredentialDataFromDb, InsertTokenDataToDb, RemoveTokenDataFromDb, GetTokenDataByTokenCodeFromDb, GetTokenDataByRenewCodeFromDb, RemoveOtherTokenDataByLoginNameFromDb, null);
        }

        public ApiResultObject<Inventec.Token.Core.TokenData> Login(HttpActionContext httpActionContext)
        {
            ApiResultObject<Inventec.Token.Core.TokenData> result = new ApiResultObject<Inventec.Token.Core.TokenData>(null, false);
            try
            {
                Inventec.Token.Core.TokenData token = authManager.Login(httpActionContext, param);
                if (token != null)
                {
                    token.RoleDatas = GetRoleData(token);
                    result = new ApiResultObject<Inventec.Token.Core.TokenData>(token, true);
                }
                else
                {
                    result.SetValue(null, false, param);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new ApiResultObject<Inventec.Token.Core.TokenData>(null, false);
            }
            return result;
        }

        private List<RoleData> GetRoleData(Inventec.Token.Core.TokenData currentToken)
        {
            List<RoleData> result = null;
            try
            {
                ACS_USER user = null;
                bool valid = (AcsUserCheckVerifyValidDataForAuthorize.Verify(param, ref user, currentToken.User.LoginName));
                if (valid)
                {
                    AcsTokenAuthenticationSDO authenticationSDO = new AcsTokenAuthenticationSDO();
                    authenticationSDO = Mapper.Map<ACS_USER, AcsTokenAuthenticationSDO>(user);
                    authenticationSDO.ApplicationCode = currentToken.User.ApplicationCode;
                    authenticationSDO.AppVersion = currentToken.VersionApp;
                    var appRoleIds = new AuthorizeRoleForUsers(param, authenticationSDO).RunWithoutApplicationRole();
                    if (appRoleIds != null && appRoleIds.Count > 0)
                    {
                        AcsRoleFilterQuery filter = new AcsRoleFilterQuery();
                        filter.IDs = appRoleIds;
                        var roles = new AcsRoleBO().Get<List<ACS_ROLE>>(filter);
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

        public ApiResultObject<Inventec.Token.Core.TokenData> GetAuthenticated(HttpActionContext httpActionContext)
        {
            ApiResultObject<Inventec.Token.Core.TokenData> result = new ApiResultObject<Inventec.Token.Core.TokenData>(null, false);
            try
            {
                string clientAddress = GetAddress(httpActionContext);
                Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => clientAddress), clientAddress));
                Inventec.Token.Core.TokenData token = null;
                var headers = httpActionContext.ControllerContext.Request.Headers;
                if (headers.Contains(HeaderConstants.TOKEN_PARAM) && headers.Contains(HeaderConstants.APPLICATION_CODE_PARAM))
                {
                    token = authManager.GetAuthenticated(httpActionContext, param);
                }
                else if (WebConfig.IS_USING_SSO && headers.Contains(MACHINE_NAME_PARAM) && headers.Contains(HeaderConstants.APPLICATION_CODE_PARAM))
                {
                    string machineName = headers.GetValues(MACHINE_NAME_PARAM).First();
                    var tokenData = this.GetTokenDataByMachineNameFromDb(machineName, param);
                    if (tokenData != null)
                    {
                        Mapper.CreateMap<ExtraTokenData, Inventec.Token.Core.TokenData>();
                        token = Mapper.Map<Inventec.Token.Core.TokenData>(tokenData);
                    }
                }

                if (token != null)
                {
                    token.RoleDatas = GetRoleData(token);
                    result = new ApiResultObject<Inventec.Token.Core.TokenData>(token, true);
                    this.AddActivityLog(token);
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("GetAuthenticated khong thanh cong");
                    result.SetValue(null, false, param);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new ApiResultObject<Inventec.Token.Core.TokenData>(null, false);
            }
            return result;
        }

        public ApiResultObject<Inventec.Token.Core.TokenData> GetAuthenticatedByAddress(HttpActionContext httpActionContext)
        {
            ApiResultObject<Inventec.Token.Core.TokenData> result = new ApiResultObject<Inventec.Token.Core.TokenData>(null, false);
            try
            {
                Inventec.Token.Core.TokenData token = authManager.GetAuthenticatedByAddress(httpActionContext, param);
                if (token != null)
                {
                    token.RoleDatas = GetRoleData(token);
                    result = new ApiResultObject<Inventec.Token.Core.TokenData>(token, true);
                    this.AddActivityLog(token);
                }
                else
                {
                    result.SetValue(null, false, param);
                    Inventec.Common.Logging.LogSystem.Debug("acs => GetAuthenticatedByAddress => fail." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new ApiResultObject<Inventec.Token.Core.TokenData>(null, false);
            }
            return result;
        }

        public ApiResultObject<Inventec.Token.Core.TokenData> Renew(HttpActionContext httpActionContext)
        {
            ApiResultObject<Inventec.Token.Core.TokenData> result = new ApiResultObject<Inventec.Token.Core.TokenData>(null, false);
            try
            {
                Inventec.Token.Core.TokenData token = authManager.Renew(httpActionContext, param);
                if (token != null)
                {
                    result = new ApiResultObject<Inventec.Token.Core.TokenData>(token, true);
                }
                else
                {
                    result.SetValue(null, false, param);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new ApiResultObject<Inventec.Token.Core.TokenData>(null, false);
            }
            return result;
        }

        public ApiResultObject<bool> ChangePassword(HttpActionContext httpActionContext)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
            try
            {
                bool resultData = authManager.ChangePassword(httpActionContext, param);
                if (resultData)
                {
                    result = new ApiResultObject<bool>(resultData, true);
                }
                else
                {
                    result.SetValue(false, false, param);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public ApiResultObject<bool> Logout(HttpActionContext httpActionContext)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
            try
            {
                bool resultData = authManager.Logout(httpActionContext, param);
                if (resultData)
                {
                    result = new ApiResultObject<bool>(resultData, true);
                }
                else
                {
                    result.SetValue(false, false, param);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// Kiem tra xem token co hop le hay khong, neu hop le thi tra ve du lieu token
        /// API nay se duoc goi boi "resource system"
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="commonParam"></param>
        /// <returns></returns>
        public ApiResultObject<CredentialData> GetCredentialData(HttpActionContext httpActionContext)
        {
            ApiResultObject<CredentialData> result = new ApiResultObject<CredentialData>(null, false);
            try
            {
                CredentialData token = authManager.GetCredentialData(httpActionContext, param);
                if (token != null)
                {
                    result = new ApiResultObject<CredentialData>(token, true);
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("GetAuthenticated khong thanh cong");
                    result.SetValue(null, false, param);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new ApiResultObject<CredentialData>(null, false);
            }
            return result;
        }

        /// <summary>
        /// Thiet lap du lieu credential data
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="commonParam"></param>
        /// <returns></returns>
        public ApiResultObject<bool> SetCredentialData(HttpActionContext actionContext, CredentialData credentialData, CommonParam commonParam)
        {
            ApiResultObject<bool> result = null;
            try
            {
                bool data = authManager.SetCredentialData(actionContext, credentialData, param);
                if (data)
                {
                    result = new ApiResultObject<bool>(data, true);
                }
                else
                {
                    result = new ApiResultObject<bool>(data, false);
                }
            }
            catch (Exception ex)
            {
                result = new ApiResultObject<bool>(false, false);
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public List<AcsCredentialTrackingSDO> GetTokenDataAlives(string appCode)
        {
            List<AcsCredentialTrackingSDO> extraTokenDatas = new List<AcsCredentialTrackingSDO>();
            try
            {
                var rs = new ACS.MANAGER.AcsToken.AcsTokenManager().GetAlives(appCode);
                if (IsNotNullOrEmpty(rs))
                {
                    foreach (var item in rs)
                    {
                        extraTokenDatas.Add(new AcsCredentialTrackingSDO()
                        {
                            ApplicationCode = item.User.ApplicationCode,
                            Email = item.User.Email,
                            GCode = item.User.GCode,
                            LoginName = item.User.LoginName,
                            Mobile = item.User.Mobile,
                            UserName = item.User.UserName,
                            ExpireTime = item.ExpireTime,
                            LastAccessTime = item.LastAccessTime,
                            LoginAddress = item.LoginAddress,
                            LoginTime = item.LoginTime,
                            MachineName = item.MachineName,
                            RenewCode = item.RenewCode,
                            TokenCode = item.TokenCode,
                            ValidAddress = item.ValidAddress,
                            VersionApp = item.VersionApp,
                            AuthenticationCode = item.AuthenticationCode,
                            AuthorSystemCode = item.AuthorSystemCode
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return extraTokenDatas;
        }

        public List<AcsCredentialTrackingSDO> GetTokenDataInRamAlives()
        {
            List<AcsCredentialTrackingSDO> extraTokenDatas = new List<AcsCredentialTrackingSDO>();
            try
            {
                var rs = authManager.GetTokenDataAlives("");
                if (IsNotNullOrEmpty(rs))
                {
                    foreach (var item in rs)
                    {
                        extraTokenDatas.Add(new AcsCredentialTrackingSDO()
                        {
                            ApplicationCode = item.User.ApplicationCode,
                            Email = item.User.Email,
                            GCode = item.User.GCode,
                            LoginName = item.User.LoginName,
                            Mobile = item.User.Mobile,
                            UserName = item.User.UserName,
                            ExpireTime = item.ExpireTime,
                            LastAccessTime = item.LastAccessTime,
                            LoginAddress = item.LoginAddress,
                            LoginTime = item.LoginTime,
                            MachineName = item.MachineName,
                            RenewCode = item.RenewCode,
                            TokenCode = item.TokenCode,
                            ValidAddress = item.ValidAddress,
                            VersionApp = item.VersionApp,
                            AuthenticationCode = item.AuthenticationCode,
                            AuthorSystemCode = item.AuthorSystemCode
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return extraTokenDatas;
        }

        public AcsCredentialTrackingSDO GetTokenDataAlivesUser(AcsCredentialTrackingSDO credential)
        {
            AcsCredentialTrackingSDO extraTokenData = new AcsCredentialTrackingSDO();
            try
            {
                var credentials = GetTokenDataAlives(credential.ApplicationCode);
                if (IsNotNullOrEmpty(credentials))
                {
                    extraTokenData = credentials.FirstOrDefault(o => o.LoginName == credential.LoginName);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return extraTokenData;
        }

        /// <summary>
        /// Thiet lap du lieu credential data
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="commonParam"></param>
        /// <returns></returns>
        public ApiResultObject<bool> SetCredentialAlive(HttpActionContext actionContext, List<Inventec.Token.Core.TokenData> tokens, CommonParam commonParam)
        {
            ApiResultObject<bool> result = null;
            try
            {
                bool data = new ACS.MANAGER.AcsToken.AcsTokenManager().UpdateAlive(tokens);
                if (data)
                {
                    result = new ApiResultObject<bool>(data, true);
                }
                else
                {
                    result = new ApiResultObject<bool>(data, false);
                }
            }
            catch (Exception ex)
            {
                result = new ApiResultObject<bool>(false, false);
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public void InitTokenInActiveRamForStartApp()
        {
            try
            {
                //Inventec.Token.AuthSystem.TokenStore.TOKEN_INACTIVE_DATA = new Dictionary<string, ExtraTokenData>();//TODO
                var currentTokenAlls = new ACS.MANAGER.AcsToken.AcsTokenManager().GetUnAlives("");
                if (currentTokenAlls != null && currentTokenAlls.Count > 0)
                {
                    Inventec.Common.Logging.LogSystem.Info("Khoi tao app hoac tien trinh dong bo du lieu token giua cac ACS, lay du lieu bang token tim thay " + currentTokenAlls.Count + " token da inactive.");
                    //Inventec.Token.AuthSystem.TokenStore.TOKEN_INACTIVE_DATA = currentTokenAlls.ToDictionary(o => o.TokenCode, o => o);//TODO
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void InitTokenInRamForStartApp(List<ExtraTokenData> tokenAlls)
        {
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("InitTokenInRamForStartApp.1");
                var currentTokenAlls = (tokenAlls != null && tokenAlls.Count > 0) ? tokenAlls : new ACS.MANAGER.AcsToken.AcsTokenManager().GetAlives("");
                if (currentTokenAlls != null && currentTokenAlls.Count > 0)
                {
                    Inventec.Common.Logging.LogSystem.Info("Khoi tao app hoac tien trinh dong bo du lieu token giua cac ACS, lay du lieu bang token tim thay " + currentTokenAlls.Count + " token hop le.");
                    authManager.InitialTokenForStart(currentTokenAlls);
                    var tokenInRam = authManager.GetTokenDataAlives("");
                    Inventec.Common.Logging.LogSystem.Info("Da dong bo du lieu token vua get duoc vao RAM__Kiem tra lai so luong token trong ram =" + (tokenInRam != null ? tokenInRam.Count : 0));
                }
                Inventec.Common.Logging.LogSystem.Debug("InitTokenInRamForStartApp.2");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public bool InsertTokenInRam(ExtraTokenData tokenInsert)
        {
            bool success = false;
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("InsertTokenInRam.1");
                List<ExtraTokenData> tokenInserts = new List<ExtraTokenData>();
                tokenInserts.Add(tokenInsert);

                Inventec.Common.Logging.LogSystem.Info("Khoi tao app hoac tien trinh dong bo du lieu token giua cac ACS, lay du lieu bang token tim thay " + tokenInserts.Count + " token hop le.");
                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => tokenInsert.TokenCode), tokenInsert.TokenCode));
                success = authManager.InitialTokenForStart(tokenInserts);
                var tokenInRam = authManager.GetTokenDataAlives("");
                Inventec.Common.Logging.LogSystem.Info("Da dong bo du lieu token vua get duoc vao RAM__Kiem tra lai so luong token trong ram TOKEN_DATA.Count:" + (tokenInRam != null ? tokenInRam.Count : 0));

                Inventec.Common.Logging.LogSystem.Debug("InsertTokenInRam.2");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return success;
        }

        public bool RemoveTokenInRam(List<string> tokenCodes)
        {
            bool success = false;
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("RemoveTokenInRam.1");
                success = authManager.RemoveTokenInRam(tokenCodes);
                Inventec.Common.Logging.LogSystem.Debug("RemoveTokenInRam.2");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return success;
        }

        public ApiResultObject<Inventec.Token.Core.TokenData> CloneTokenWithApp(HttpActionContext httpActionContext)
        {
            Inventec.Common.Logging.LogSystem.Debug("CloneTokenWithApp.1");
            ApiResultObject<Inventec.Token.Core.TokenData> result = new ApiResultObject<Inventec.Token.Core.TokenData>(null, false);
            try
            {
                var headers = httpActionContext.ControllerContext.Request.Headers;
                if (headers.Contains(ApplicationConfig.SECURE_KEY_PARAM) && headers.Contains(ApplicationConfig.APPLICATION_CODE_PARAM) && headers.Contains(ApplicationConfig.TOKEN_CODE_PARAM))
                {
                    Inventec.Common.Logging.LogSystem.Debug("CloneTokenWithApp.2");
                    string securekey = headers.GetValues(ApplicationConfig.SECURE_KEY_PARAM).First();
                    string applicationCode = headers.GetValues(ApplicationConfig.APPLICATION_CODE_PARAM).First();
                    string tokenCode = headers.GetValues(ApplicationConfig.TOKEN_CODE_PARAM).First();

                    Inventec.Token.Core.TokenData token = authManager.GetAuthenticated(httpActionContext, param);
                    if (token != null)
                    {
                        Inventec.Common.Logging.LogSystem.Debug("CloneTokenWithApp.3");
                        ExtraTokenData tokenNew = new ExtraTokenData();
                        tokenNew.AuthenticationCode = token.AuthenticationCode;
                        tokenNew.AuthorSystemCode = token.AuthorSystemCode;
                        tokenNew.ExpireTime = DateTime.Now.AddMinutes(WebConfig.AuthSystem__TokenTimeout);
                        tokenNew.LastAccessTime = DateTime.Now;
                        tokenNew.LoginAddress = GetAddress(httpActionContext);
                        tokenNew.LoginTime = DateTime.Now;
                        tokenNew.MachineName = Environment.MachineName;
                        tokenNew.VersionApp = token.VersionApp;
                        tokenNew.User = token.User;
                        tokenNew.User.ApplicationCode = applicationCode;
                        tokenNew.ValidAddress = tokenNew.LoginAddress;
                        tokenNew.TokenCode = GenerateTokenCode(tokenNew);
                        tokenNew.RenewCode = GenerateRenewCode(tokenNew.TokenCode);

                        CommonParam paramCreate = new CommonParam();
                        bool success = InsertTokenDataToDb(tokenNew, paramCreate);
                        if (success)
                        {
                            Inventec.Common.Logging.LogSystem.Debug("CloneTokenWithApp.4");
                            Inventec.Token.Core.TokenData tokenResult = new Inventec.Token.Core.TokenData();
                            tokenResult.AuthenticationCode = tokenNew.AuthenticationCode;
                            tokenResult.AuthorSystemCode = tokenNew.AuthorSystemCode;
                            tokenResult.ExpireTime = tokenNew.ExpireTime;
                            tokenResult.LastAccessTime = tokenNew.LastAccessTime;
                            tokenResult.LoginAddress = tokenNew.LoginAddress;
                            tokenResult.LoginTime = tokenNew.LoginTime;
                            tokenResult.MachineName = tokenNew.MachineName;
                            tokenResult.TokenCode = tokenNew.TokenCode;
                            tokenResult.RenewCode = tokenNew.RenewCode;
                            tokenResult.VersionApp = tokenNew.VersionApp;
                            tokenResult.User = tokenNew.User;

                            result = new ApiResultObject<Inventec.Token.Core.TokenData>(tokenResult, true);
                            Inventec.Common.Logging.LogSystem.Debug("CloneTokenWithApp.5");
                            InsertTokenInRam(tokenNew);
                            InitThreadSyncTokenInsert(tokenNew);
                            Inventec.Common.Logging.LogSystem.Debug("CloneTokenWithApp.6");
                            LogEventLoginBySecretKeySuccess(tokenResult);
                            Inventec.Common.Logging.LogSystem.Debug("CloneTokenWithApp.7");
                        }
                    }
                    else
                    {
                        Inventec.Common.Logging.LogSystem.Debug("CloneTokenWithApp khong thanh cong");
                        param.Messages.Add("Token gửi lên không hợp lệ");
                        result.SetValue(null, false, param);
                    }
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("CloneTokenWithApp fail, client truyền thiếu dữ liệu bắt buộc");
                    param.Messages.Add("Thiếu trường dữ liệu bắt buộc");
                    result.SetValue(null, false, param);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            Inventec.Common.Logging.LogSystem.Debug("CloneTokenWithApp.8");
            return result;
        }

        async Task LogEventLoginBySecretKeySuccess(Inventec.Token.Core.TokenData tokenData)
        {
            try
            {
                CommonParam paramDNTC = new CommonParam();
                MessageUtil.SetMessage(paramDNTC, LibraryMessage.Message.Enum.Core_AcsUser_DangNhapThanhCong);
                SdaEventLogSDO eventLog = new SdaEventLogSDO();

                eventLog.Description = String.Format(paramDNTC.GetMessage(), tokenData.User.ApplicationCode);
                eventLog.EventTime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                //if (!String.IsNullOrEmpty(entity.IP))
                //    eventLog.Ip = entity.IP;
                //else
                eventLog.Ip = tokenData.LoginAddress;
                eventLog.LogginName = tokenData.User.LoginName;
                eventLog.AppCode = tokenData.User.ApplicationCode;
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

        private string GetAddress(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            string address = "";
            try
            {
                var myRequest = ((System.Web.HttpContextWrapper)actionContext.Request.Properties["MS_HttpContext"]).Request;
                address = myRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(address))
                {
                    string[] ipRange = address.Split(',');
                    int le = ipRange.Length - 1;
                    address = ipRange[le];
                }
                else
                {
                    address = myRequest.ServerVariables["REMOTE_ADDR"];
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return address;
        }

        public string GenerateTokenCode(ExtraTokenData tokenData)
        {
            if (WebConfig.IsUsingJWT)
            {
                return authManager.GenerateTokenCode(tokenData);
            }
            else
                return ACS.UTILITY.GenerateToken.GenerateTokenCode(tokenData.User.LoginName, tokenData.LoginAddress);
        }

        public string GenerateRenewCode(string tokenCode)
        {
            if (WebConfig.IsUsingJWT)
            {
                return authManager.GenerateRenewCode(tokenCode);
            }
            else
                return ACS.UTILITY.GenerateToken.GenerateRenewCode(tokenCode);
        }

        public void InitJwtKeyForStartApp()
        {
            try
            {
                if (WebConfig.IsUsingJWT)
                {
                    RSAKeyProvider rSAKeyProvider = new RSAKeyProvider();
                    string rsaKey = rSAKeyProvider.GetPrivateAndPublicKey(ConstantHash.HASH_SALT);
                    string rsaPublicKey = rSAKeyProvider.GetPublicKey();
                    if (String.IsNullOrEmpty(rsaKey))
                    {
                        Inventec.Common.Logging.LogSystem.Warn("Khoi tao Jwt Key that bai: " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => rsaKey), rsaKey));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public ApiResultObject<bool> RemoveOtherSession(HttpActionContext httpActionContext)
        {
            Inventec.Common.Logging.LogSystem.Debug("RemoveOtherSession.1");
            ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
            bool success = false;
            try
            {
                var headers = httpActionContext.ControllerContext.Request.Headers;
                if (headers.Contains(HeaderConstants.APPLICATION_CODE_PARAM) && headers.Contains(ApplicationConfig.TOKEN_CODE_PARAM))
                {
                    Inventec.Common.Logging.LogSystem.Debug("RemoveOtherSession.2");
                    Inventec.Token.Core.TokenData token = Inventec.Token.ResourceSystem.ResourceTokenManager.GetTokenData();
                    if (token != null)
                    {
                        Inventec.Common.Logging.LogSystem.Debug("RemoveOtherSession.3");
                        authManager.RemoveOtherSession(httpActionContext, param);
                        success = true;
                    }
                    else
                    {
                        Inventec.Common.Logging.LogSystem.Warn("RemoveOtherSession.4: get token data khong thanh cong");
                        param.Messages.Add("Token gửi lên không hợp lệ");
                        success = false;
                    }
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Warn("RemoveOtherSession.5: fail, client truyền thiếu dữ liệu bắt buộc");
                    param.Messages.Add("Thiếu trường dữ liệu bắt buộc");
                    success = false;
                }
                result.SetValue(success, success, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            Inventec.Common.Logging.LogSystem.Debug("RemoveOtherSession.6");
            return result;
        }
    }
}
