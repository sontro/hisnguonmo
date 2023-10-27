using ACS.EFMODEL.DataModels;
using ACS.LibraryConfig;
using ACS.MANAGER.AcsToken;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsCredentialData.Get;
using ACS.MANAGER.Core.TokenSys;
using ACS.MANAGER.Core.TokenSys.Authentication;
using ACS.MANAGER.Manager;
using ACS.SDO;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using Inventec.Token.AuthSystem;
using Inventec.Token.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACS.MANAGER.Token
{
    public sealed partial class TokenManager : Inventec.Backend.MANAGER.BusinessBase
    {
        /// <summary>
        /// Lay user hop le dua vao thong tin loginName, password, applicationCode
        /// Nghiep vu xac dinh the nao la "hop le" la do he thong implement thu vien nay quyet dinh
        /// </summary>
        /// <param name="loginName">Ten dang nhap</param>
        /// <param name="password">Mat khau</param>
        /// <param name="applicationCode">Ma ung dung</param>
        /// <param name="commonParam"></param>
        /// <returns></returns>
        internal UserData GetValidUserData(string loginName, string password, string applicationCode, string appVersion, CommonParam commonParam)
        {
            UserData result = null;
            try
            {
                AcsTokenLoginSDO loginSDO = new AcsTokenLoginSDO();
                loginSDO.APPLICATION_CODE = applicationCode;
                loginSDO.LOGIN_NAME = loginName;
                loginSDO.PASSWORD = password;
                loginSDO.APP_VERSION = appVersion;
                commonParam = new CommonParam();
                ACS_USER userData = (ACS_USER)new AcsTokenManagerExtra(commonParam).Login(loginSDO);
                param = commonParam;
                if (userData != null)
                {
                    result = new UserData();
                    result.ApplicationCode = applicationCode;
                    result.Email = userData.EMAIL;
                    result.GCode = userData.G_CODE;
                    result.LoginName = userData.LOGINNAME;
                    result.Mobile = userData.MOBILE;
                    result.UserName = userData.USERNAME;
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("Khong tim thay du lieu AcsUser. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => loginName), loginName) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => applicationCode), applicationCode));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Lay user hop le dua vao thong tin loginName, applicationCode
        /// Nghiep vu xac dinh the nao la "hop le" la do he thong implement thu vien nay quyet dinh
        /// </summary>
        /// <param name="loginName">Ten dang nhap</param>
        /// <param name="applicationCode">Ma ung dung</param>
        /// <param name="commonParam"></param>
        /// <returns></returns>
        internal bool IsGrantedUser(string loginName, string applicationCode, CommonParam commonParam)
        {
            bool result = false;
            try
            {
                TokenSysAuthenticationResourceSDO loginSDO = new TokenSysAuthenticationResourceSDO();
                loginSDO.ApplicationCode = applicationCode;
                loginSDO.LoginName = loginName;
                commonParam = new CommonParam();
                ACS_USER userData = (ACS_USER)new AcsTokenBO().AuthenticationResource(loginSDO);
                param = commonParam;
                if (userData != null)
                {
                    result = true;
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Warn("IsGrantedUser: Khong tim thay du lieu AcsUser. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => loginName), loginName) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => applicationCode), applicationCode));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Cap nhat mat khau cho user
        /// </summary>
        /// <param name="loginName">Ten dang nhap</param>
        /// <param name="oldPassword">Mat khau cu</param>
        /// <param name="newPassword">Mat khau moi</param>
        /// <param name="commonParam"></param>
        /// <returns></returns>
        public bool UpdateUserPasswordToDb(string loginName, string oldPassword, string newPassword, CommonParam commonParam)
        {
            bool result = false;
            try
            {
                if (!IsNotNullOrEmpty(loginName)) throw new ArgumentNullException("loginName");
                if (!IsNotNullOrEmpty(oldPassword)) throw new ArgumentNullException("oldPassword");
                if (!IsNotNullOrEmpty(newPassword)) throw new ArgumentNullException("newPassword");

                commonParam = new CommonParam();
                AcsUserChangePasswordSDO changpassSDO = new AcsUserChangePasswordSDO();
                changpassSDO.LOGIN_NAME = loginName;
                changpassSDO.PASSWORD__NEW = newPassword;
                changpassSDO.PASSWORD__OLD = oldPassword;
                result = (bool)new AcsUserManager(commonParam).ChangePassword(changpassSDO);
                param = commonParam;
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

        /// <summary>
        /// Lay du lieu credential data
        /// </summary>
        /// <param name="resourceSystemCode"></param>
        /// <param name="tokenCode"></param>
        /// <param name="dataKey"></param>
        /// <param name="commonParam"></param>
        /// <returns></returns>
        public CredentialData GetCredentialDataFromDb(string resourceSystemCode, string tokenCode, string dataKey, CommonParam commonParam)
        {
            CredentialData result = null;
            try
            {
                commonParam = new CommonParam();
                AcsCredentialDataFilterForTokenManager filter = new AcsCredentialDataFilterForTokenManager();
                filter.RESOURCE_SYSTEM_CODE = resourceSystemCode;
                filter.TOKEN_CODE = tokenCode;
                filter.DATA_KEY = dataKey;
                filter.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;

                ACS_CREDENTIAL_DATA credentialData = (ACS_CREDENTIAL_DATA)new AcsCredentialDataManager(commonParam).Get<ACS_CREDENTIAL_DATA>(filter);
                param = commonParam;
                if (credentialData != null)
                {
                    result = new CredentialData();
                    result.Data = credentialData.DATA;
                    result.DataKey = credentialData.DATA_KEY;
                    result.ResourceSystemCode = credentialData.RESOURCE_SYSTEM_CODE;
                    result.TokenCode = credentialData.TOKEN_CODE;
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("Khong tim thay du lieu ACS_CREDENTIAL_DATA. "
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => resourceSystemCode), resourceSystemCode)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => tokenCode), tokenCode)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataKey), dataKey)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => filter), filter)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => commonParam), commonParam));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Xoa du lieu credential data
        /// </summary>
        /// <param name="resourceSystemCode"></param>
        /// <param name="tokenCode"></param>
        /// <param name="dataKey"></param>
        /// <param name="commonParam"></param>
        /// <returns></returns>
        public bool InsertCredentialDataToDb(CredentialData credentialData, CommonParam commonParam)
        {
            bool result = false;
            try
            {
                if (!IsNotNull(credentialData)) throw new ArgumentNullException("credentialData is null");

                ACS_CREDENTIAL_DATA entity = new ACS_CREDENTIAL_DATA();
                entity.RESOURCE_SYSTEM_CODE = credentialData.ResourceSystemCode;
                entity.TOKEN_CODE = credentialData.TokenCode;
                entity.DATA_KEY = credentialData.DataKey;
                entity.DATA = credentialData.Data;

                commonParam = new CommonParam();
                ACS_CREDENTIAL_DATA data = (ACS_CREDENTIAL_DATA)new AcsCredentialDataManager(commonParam).Create(entity);
                param = commonParam;
                if (data != null)
                {
                    result = true;
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("InsertCredentialDataToDb that bai. "
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => credentialData), credentialData));
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

        /// <summary>
        /// Xoa du lieu credential data
        /// </summary>
        /// <param name="resourceSystemCode"></param>
        /// <param name="tokenCode"></param>
        /// <param name="dataKey"></param>
        /// <param name="commonParam"></param>
        /// <returns></returns>
        public bool DeleteCredentialDataFromDb(string resourceSystemCode, string tokenCode, string dataKey, CommonParam commonParam)
        {
            bool result = false;
            try
            {
                if (!IsNotNullOrEmpty(resourceSystemCode)) throw new ArgumentNullException("resourceSystemCode is null");
                if (!IsNotNullOrEmpty(tokenCode)) throw new ArgumentNullException("tokenCode is null");
                if (!IsNotNullOrEmpty(dataKey)) throw new ArgumentNullException("dataKey is null");

                AcsCredentialDataFilterForTokenManager filter = new AcsCredentialDataFilterForTokenManager();
                filter.RESOURCE_SYSTEM_CODE = resourceSystemCode;
                filter.TOKEN_CODE = tokenCode;
                filter.DATA_KEY = dataKey;
                filter.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
                commonParam = new CommonParam();

                ACS_CREDENTIAL_DATA entity = (ACS_CREDENTIAL_DATA)new AcsCredentialDataManager(commonParam).Get<ACS_CREDENTIAL_DATA>(filter);
                param = commonParam;
                if (entity != null)
                {
                    result = (new AcsCredentialDataManager(commonParam).Lock(entity) != null);
                    if (!result)
                    {
                        Inventec.Common.Logging.LogSystem.Debug("DeleteCredentialDataFromDb that bai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                    }
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("Khong tim thay du lieu ACS_CREDENTIAL_DATA de xoa. "
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => resourceSystemCode), resourceSystemCode)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => tokenCode), tokenCode)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataKey), dataKey)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => filter), filter)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => commonParam), commonParam));
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

        /// <summary>
        /// Xoa tat ca cac credential data theo tokenCode khi logout
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <param name="commonParam"></param>
        /// <returns></returns>
        public bool DeleteAllCredentialDataFromDb(string tokenCode, CommonParam commonParam)
        {
            bool result = false;
            try
            {
                if (!IsNotNullOrEmpty(tokenCode)) throw new ArgumentNullException("tokenCode is null");

                AcsCredentialDataFilterQuery filter = new AcsCredentialDataFilterQuery();
                filter.TOKEN_CODE = tokenCode;
                filter.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
                commonParam = new CommonParam();
                List<ACS_CREDENTIAL_DATA> entityCredentialDeletes = new AcsCredentialDataManager(commonParam).Get<List<ACS_CREDENTIAL_DATA>>(filter);
                if (entityCredentialDeletes != null && entityCredentialDeletes.Count > 0)
                {
                    result = (new AcsCredentialDataManager(commonParam).Lock(entityCredentialDeletes) != null);
                    if (!result)
                    {
                        Inventec.Common.Logging.LogSystem.Debug("DeleteAllCredentialDataFromDb that bai. "
                            + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entityCredentialDeletes), entityCredentialDeletes));
                    }
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("Khong tim thay du lieu ACS_CREDENTIAL_DATA de xoa. "
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => tokenCode), tokenCode)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => filter), filter)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => commonParam), commonParam));
                }
                param = commonParam;
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

        public bool InsertTokenDataToDb(ExtraTokenData extraTokenData, CommonParam commonParam)
        {
            bool result = false;
            try
            {
                if (!IsNotNull(extraTokenData)) throw new ArgumentNullException("extraTokenData is null");
                if (!IsNotNull(extraTokenData.User)) throw new ArgumentNullException("User is null");
                if (!IsNotNull(extraTokenData.TokenCode)) throw new ArgumentNullException("TokenCode is null");

                ACS_TOKEN entity = new ACS_TOKEN()
                {
                    APP_VERSION = extraTokenData.VersionApp,
                    APPLICATION_CODE = extraTokenData.User.ApplicationCode,
                    EMAIL = extraTokenData.User.Email,
                    EXPIRE_TIME = Convert.ToInt64(extraTokenData.ExpireTime.ToString("yyyyMMddHHmmss")),
                    LAST_ACCESS_TIME = Convert.ToInt64(extraTokenData.LastAccessTime.ToString("yyyyMMddHHmmss")),
                    LOGIN_ADDRESS = extraTokenData.LoginAddress,
                    LOGIN_NAME = extraTokenData.User.LoginName,
                    LOGIN_TIME = Convert.ToInt64(extraTokenData.LoginTime.ToString("yyyyMMddHHmmss")),
                    MACHINE_NAME = extraTokenData.MachineName,
                    MOBILE = extraTokenData.User.Mobile,
                    RENEW_CODE = extraTokenData.RenewCode,
                    TOKEN_CODE = extraTokenData.TokenCode,
                    USER_NAME = extraTokenData.User.UserName
                };

                commonParam = new CommonParam();
                var rs = new AcsTokenManager(commonParam).Create(entity);
                ACS_TOKEN data = (rs != null ? rs.Data : null);
                param = commonParam;
                if (data != null)
                {
                    result = true;
                    InitThreadSyncTokenInsert(extraTokenData);
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

        public bool RemoveTokenDataFromDb(string tokenCode, CommonParam commonParam)
        {
            bool result = false;
            try
            {
                if (!IsNotNull(tokenCode)) throw new ArgumentNullException("tokenCode is null");

                var rs = new AcsTokenManager(commonParam).Get(new AcsTokenFilterQuery() { TokenCode = tokenCode, IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE });
                ACS_TOKEN oldToken = (rs != null ? rs.Data.FirstOrDefault() : null);
                if (oldToken != null && oldToken.ID > 0)
                {
                    var rsNew = new AcsTokenManager(commonParam).Lock(oldToken.ID);
                    result = (rsNew != null && rsNew.Data != null ? true : false);
                    if (result)
                    {
                        var extToken = GenerateExtraToken(oldToken);
                        LogoutActivityLog(extToken);
                        //TODO
                        //if (Inventec.Token.AuthSystem.TokenStore.TOKEN_INACTIVE_DATA != null && !Inventec.Token.AuthSystem.TokenStore.TOKEN_INACTIVE_DATA.ContainsKey(oldToken.TOKEN_CODE))
                        //    Inventec.Token.AuthSystem.TokenStore.TOKEN_INACTIVE_DATA.Add(oldToken.TOKEN_CODE, extToken);
                        InitThreadSyncTokenDelete(oldToken.TOKEN_CODE);
                    }
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("Khong tim thay du lieu ACS_TOKEN de update isactive ve 0(trang thai danh dau da xoa, tien trinh ngam se quet va xoa sau)."
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => tokenCode), tokenCode)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => commonParam), commonParam));
                }
                param = commonParam;
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

        public ExtraTokenData GetTokenDataByTokenCodeFromDb(string tokenCode, CommonParam commonParam)
        {
            ExtraTokenData result = null;
            try
            {
                if (!IsNotNull(tokenCode)) throw new ArgumentNullException("tokenCode is null");

                //if (WebConfig.IsNotGetTkenFromDB == "1")
                //{
                //    Inventec.Common.Logging.LogSystem.Debug("GetTokenDataByTokenCodeFromDb: Không get token từ DB khi chạy api GetAuthenticated|GetAuthenticatedByAddress và kiểm tra token truyền lên không có trong RAM của ACS"
                //        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => WebConfig.IsNotGetTkenFromDB), WebConfig.IsNotGetTkenFromDB));
                //    return null;
                //}

                var rs = new AcsTokenManager(commonParam).Get(new AcsTokenFilterQuery() { TokenCode = tokenCode, IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE });
                ACS_TOKEN oldToken = (rs != null ? rs.Data.FirstOrDefault() : null);
                if (oldToken != null)
                {
                    result = GenerateExtraToken(oldToken);
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("Khong tim thay du lieu ACS_TOKEN."
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => tokenCode), tokenCode)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => commonParam), commonParam));
                }
                param = commonParam;
            }
            catch (ArgumentNullException ex)
            {
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public ExtraTokenData GetTokenDataByRenewCodeFromDb(string renewCode, CommonParam commonParam)
        {
            ExtraTokenData result = null;
            try
            {
                if (!IsNotNull(renewCode)) throw new ArgumentNullException("tokenCode is null");

                //if (WebConfig.IsNotGetTkenFromDB == "1")
                //{
                //    Inventec.Common.Logging.LogSystem.Debug("GetTokenDataByRenewCodeFromDb: Không get token từ DB khi chạy api GetAuthenticated|GetAuthenticatedByAddress và kiểm tra token truyền lên không có trong RAM của ACS"
                //        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => WebConfig.IsNotGetTkenFromDB), WebConfig.IsNotGetTkenFromDB));
                //    return null;
                //}

                var rs = new AcsTokenManager(commonParam).Get(new AcsTokenFilterQuery() { RenewCode = renewCode, IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE });
                ACS_TOKEN oldToken = (rs != null ? rs.Data.FirstOrDefault() : null);
                if (oldToken != null)
                {
                    result = GenerateExtraToken(oldToken);
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("Khong tim thay du lieu ACS_TOKEN."
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => renewCode), renewCode)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => commonParam), commonParam));
                }
                param = commonParam;
            }
            catch (ArgumentNullException ex)
            {
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public ExtraTokenData GetTokenDataByMachineNameFromDb(string machineName, CommonParam commonParam)
        {
            ExtraTokenData result = null;
            try
            {
                if (!IsNotNull(machineName)) throw new ArgumentNullException("machineName is null");

                var rs = new AcsTokenManager(commonParam).Get(new AcsTokenFilterQuery() { MachineName = machineName, IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE, ORDER_FIELD = "EXPIRE_TIME", ORDER_DIRECTION = "DESC" });
                Inventec.Common.Logging.LogSystem.Debug("GetTokenDataByMachineNameFromDb:" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => rs), rs));
                ACS_TOKEN oldToken = (rs != null ? rs.Data.FirstOrDefault() : null);
                if (oldToken != null)
                {
                    result = GenerateExtraToken(oldToken);
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("Khong tim thay du lieu ACS_TOKEN."
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => machineName), machineName)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => commonParam), commonParam));
                }
                param = commonParam;
            }
            catch (ArgumentNullException ex)
            {
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public bool RemoveOtherTokenDataByLoginNameFromDb(string tokenCode, string loginName, CommonParam commonParam)
        {
            bool result = false;
            try
            {
                if (!IsNotNullOrEmpty(loginName)) throw new ArgumentNullException("loginName");
                if (!IsNotNullOrEmpty(tokenCode)) throw new ArgumentNullException("tokenCode");

                commonParam = new CommonParam();
                var rs = new AcsTokenManager(commonParam).Get(new AcsTokenFilterQuery() { LoginName = loginName });
                List<ACS_TOKEN> otherTokens = (rs != null && rs.Data != null && rs.Data.Count > 0) ? rs.Data.Where(o => o.TOKEN_CODE != tokenCode).ToList() : null;
                if (otherTokens != null && otherTokens.Count > 0)
                {
                    var rsNew = new AcsTokenManager(commonParam).DeleteWithOtherSession(otherTokens);
                    result = (rsNew != null && rsNew.Data != null ? true : false);
                    if (result)
                    {
                        foreach (var otherToken in otherTokens)
                        {
                            InitThreadSyncTokenDelete(otherToken.TOKEN_CODE);
                        }
                    }
                }
                else
                {
                    result = true;
                    Inventec.Common.Logging.LogSystem.Debug("Khong tim thay du lieu ACS_TOKEN de xoa."
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => loginName), loginName)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => tokenCode), tokenCode)
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => commonParam), commonParam));
                }
                param = commonParam;
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

        internal ExtraTokenData GenerateExtraToken(ACS_TOKEN tokenData)
        {
            return new ExtraTokenData()
            {
                ExpireTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(tokenData.EXPIRE_TIME).Value,
                LastAccessTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(tokenData.LAST_ACCESS_TIME ?? 0).Value,
                LoginAddress = tokenData.LOGIN_ADDRESS,
                LoginTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(tokenData.LOGIN_TIME).Value,
                MachineName = tokenData.MACHINE_NAME,
                RenewCode = tokenData.RENEW_CODE,
                TokenCode = tokenData.TOKEN_CODE,
                User = new UserData()
                {
                    ApplicationCode = tokenData.APPLICATION_CODE,
                    Email = tokenData.EMAIL,
                    LoginName = tokenData.LOGIN_NAME,
                    Mobile = tokenData.MOBILE,
                    UserName = tokenData.USER_NAME
                },
                VersionApp = tokenData.APP_VERSION,
                ValidAddress = tokenData.LOGIN_ADDRESS,
                AuthorSystemCode = tokenData.AUTHOR_SYSTEM_CODE,
                AuthenticationCode = tokenData.AUTHENTICATION_CODE
            };
        }

        private void AddActivityLog(Inventec.Token.Core.TokenData token)
        {
            try
            {
                ACS_ACTIVITY_LOG log = new ACS_ACTIVITY_LOG();
                log.ACTIVITY_TIME = Inventec.Common.DateTime.Get.Now().Value;
                log.ACTIVITY_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_ACTIVITY_TYPE.ID__AUTHEN;
                log.EMAIL = token.User.Email;
                log.EXECUTE_LOGINNAME = token.User.LoginName;
                log.LOGINNAME = token.User.LoginName;
                log.MOBILE = token.User.Mobile;
                log.USERNAME = token.User.UserName;
                log.APPLICATION_CODE = token.User.ApplicationCode;
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

        private void LogoutActivityLog(ExtraTokenData token)
        {
            try
            {
                ACS_ACTIVITY_LOG log = new ACS_ACTIVITY_LOG();
                log.ACTIVITY_TIME = Inventec.Common.DateTime.Get.Now().Value;
                log.ACTIVITY_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_ACTIVITY_TYPE.ID__LOGOUT;
                log.EMAIL = token.User.Email;
                log.EXECUTE_LOGINNAME = token.User.LoginName;
                log.LOGINNAME = token.User.LoginName;
                log.MOBILE = token.User.Mobile;
                log.USERNAME = token.User.UserName;
                log.APPLICATION_CODE = token.User.ApplicationCode;
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

        internal void InitThreadSyncData(List<AcsCredentialTrackingSDO> tokenAlives)
        {
            try
            {
                if (WebConfig.LIST_BACKPLANE_ADDRESSES != null && WebConfig.LIST_BACKPLANE_ADDRESSES.Count > 0)
                {
                    var tokenAliveDatas = ((tokenAlives != null && tokenAlives.Count > 0) ? tokenAlives : GetTokenDataInRamAlives());
                    foreach (string address in WebConfig.LIST_BACKPLANE_ADDRESSES)
                    {
                        if (!string.IsNullOrWhiteSpace(address))
                        {
                            InitThreadSyncData(address, tokenAlives);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private async Task InitThreadSyncData(string address, List<AcsCredentialTrackingSDO> tokenAlives)
        {
            try
            {
                ApiConsumer mosConsumer = new ApiConsumer(address, "", ACS.UTILITY.Constant.APPLICATION_CODE);

                ApiResultObject<bool> result = await mosConsumer.PostAsync<ApiResultObject<bool>>("api/AcsToken/SyncToken", new CommonParam(), tokenAlives);
                Inventec.Common.Logging.LogSystem.Warn("Sync ACS token in RAM: " + address + "AcsToken/SyncToken " + ((result == null || !result.Success) ? "that bai" : "thanh cong"));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal async Task InitThreadSyncTokenInsert(ExtraTokenData extraTokenData)
        {
            try
            {
                if (WebConfig.LIST_BACKPLANE_ADDRESSES != null && WebConfig.LIST_BACKPLANE_ADDRESSES.Count > 0)
                {
                    foreach (string address in WebConfig.LIST_BACKPLANE_ADDRESSES)
                    {
                        if (!string.IsNullOrWhiteSpace(address))
                        {
                            InitThreadSyncTokenInsert(address, extraTokenData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private async Task InitThreadSyncTokenInsert(string address, ExtraTokenData extraTokenData)
        {
            try
            {
                ApiConsumer mosConsumer = new ApiConsumer(address, extraTokenData.TokenCode, ACS.UTILITY.Constant.APPLICATION_CODE);
                var tokenAlives = GetTokenDataInRamAlives();
                AcsTokenSyncInsertSDO tokenSyncInsertSDO = new AcsTokenSyncInsertSDO();

                //{
                //Token = extraTokenData,
                Inventec.Common.Mapper.DataObjectMapper.Map<AcsTokenSyncInsertSDO>(tokenSyncInsertSDO, extraTokenData);
                tokenSyncInsertSDO.TokenCount = tokenAlives != null ? tokenAlives.Count : 0;
                //};
                ApiResultObject<bool> result = await mosConsumer.PostAsync<ApiResultObject<bool>>("api/AcsToken/SyncTokenInsert", new CommonParam(), tokenSyncInsertSDO);
                Inventec.Common.Logging.LogSystem.Warn("Sync ACS token in RAM: " + address + "AcsToken/SyncTokenInsert " + ((result == null || !result.Success) ? "that bai" : "thanh cong"));
                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => tokenSyncInsertSDO), tokenSyncInsertSDO)
                   + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result)
                   + "____TokenCount = " + (tokenAlives != null ? tokenAlives.Count : 0) + "");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal async Task InitThreadSyncTokenDelete(string tokenCode)
        {
            try
            {
                if (WebConfig.LIST_BACKPLANE_ADDRESSES != null && WebConfig.LIST_BACKPLANE_ADDRESSES.Count > 0)
                {
                    foreach (string address in WebConfig.LIST_BACKPLANE_ADDRESSES)
                    {
                        if (!string.IsNullOrWhiteSpace(address))
                        {
                            InitThreadSyncTokenDelete(address, tokenCode);
                        }
                    }
                }
                Inventec.Common.Logging.LogSystem.Debug("InitThreadSyncTokenDelete____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => WebConfig.LIST_BACKPLANE_ADDRESSES), WebConfig.LIST_BACKPLANE_ADDRESSES));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private async Task InitThreadSyncTokenDelete(string address, string tokenCode)
        {
            try
            {
                ApiConsumer mosConsumer = new ApiConsumer(address, tokenCode, ACS.UTILITY.Constant.APPLICATION_CODE);
                var tokenAlives = GetTokenDataInRamAlives();
                AcsTokenSyncDeleteSDO tokenSyncDeleteSDO = new AcsTokenSyncDeleteSDO()
                {
                    TokenCode = tokenCode,
                    TokenCount = (tokenAlives != null ? tokenAlives.Count : 0)
                };
                ApiResultObject<bool> result = await mosConsumer.PostAsync<ApiResultObject<bool>>("api/AcsToken/SyncTokenDelete", new CommonParam(), tokenSyncDeleteSDO);
                Inventec.Common.Logging.LogSystem.Warn("Sync ACS token in RAM: " + address + "AcsToken/SyncTokenDelete " + ((result == null || !result.Success) ? "that bai" : "thanh cong"));
                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => tokenSyncDeleteSDO), tokenSyncDeleteSDO)
                    + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result)
                    + "____TokenCount = " + (tokenAlives != null ? tokenAlives.Count : 0) + "");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
