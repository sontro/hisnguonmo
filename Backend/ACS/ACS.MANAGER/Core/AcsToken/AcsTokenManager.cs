using Inventec.Core;
using Inventec.Common.Logging;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;
using ACS.MANAGER.Core.AcsCredentialData.Get;
using System.Linq;
using ACS.MANAGER.Manager;
using Inventec.Token.AuthSystem;
using ACS.MANAGER.Token;
using ACS.SDO;

namespace ACS.MANAGER.AcsToken
{
    public partial class AcsTokenManager : BusinessBase
    {
        public AcsTokenManager()
            : base()
        {

        }

        public AcsTokenManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<ACS_TOKEN>> Get(AcsTokenFilterQuery filter)
        {
            ApiResultObject<List<ACS_TOKEN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<ACS_TOKEN> resultData = null;
                if (valid)
                {
                    resultData = new AcsTokenGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<ACS_TOKEN> Create(ACS_TOKEN data)
        {
            ApiResultObject<ACS_TOKEN> result = new ApiResultObject<ACS_TOKEN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                ACS_TOKEN resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new AcsTokenCreate(param).Create(data);
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
        public ApiResultObject<ACS_TOKEN> Update(ACS_TOKEN data)
        {
            ApiResultObject<ACS_TOKEN> result = new ApiResultObject<ACS_TOKEN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                ACS_TOKEN resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new AcsTokenUpdate(param).Update(data);
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
        public ApiResultObject<ACS_TOKEN> ChangeLock(long id)
        {
            ApiResultObject<ACS_TOKEN> result = new ApiResultObject<ACS_TOKEN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                ACS_TOKEN resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new AcsTokenLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<ACS_TOKEN> Lock(long id)
        {
            ApiResultObject<ACS_TOKEN> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                ACS_TOKEN resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new AcsTokenLock(param).Lock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<ACS_TOKEN> Unlock(long id)
        {
            ApiResultObject<ACS_TOKEN> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                ACS_TOKEN resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new AcsTokenLock(param).Unlock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new AcsTokenTruncate(param).Truncate(id);
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
        public ApiResultObject<bool> DeleteWithOtherSession(List<ACS_TOKEN> tokens)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    CommonParam paramGet1 = new CommonParam();
                    var acsCredentialDatas = new AcsCredentialDataManager(paramGet1).Get<List<ACS_CREDENTIAL_DATA>>(new AcsCredentialDataFilterQuery() { TOKEN_CODEs = tokens.Select(k => k.TOKEN_CODE).ToList() });
                    if (acsCredentialDatas != null && acsCredentialDatas.Count > 0)
                    {
                        resultData = new AcsCredentialDataManager(new CommonParam()).Delete(acsCredentialDatas);
                        if (!resultData)
                        {
                            Inventec.Common.Logging.LogSystem.Debug("DeleteAllCredentialDataFromDb by tokenCode that bai. "
                                + Inventec.Common.Logging.LogUtil.TraceData("entityCredentialDeletes", acsCredentialDatas.Select(o => o.ID)));
                        }
                    }
                    resultData = new AcsTokenTruncate(param).TruncateList(tokens) && resultData;
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
        public void ScanToken()
        {
            try
            {
                if (ACS.LibraryConfig.WebConfig.IS_MASTER)
                {
                    Inventec.Common.Logging.LogSystem.Info("ScanToken.Begin");
                    Inventec.Common.Logging.LogSystem.Info("ScanToken.Get All AcsToken in DB=>Begin");
                    var currentTokenAlls = new AcsTokenGet(param).Get(new AcsTokenFilterQuery());
                    if (currentTokenAlls == null || currentTokenAlls.Count == 0) return;
                    Inventec.Common.Logging.LogSystem.Info("ScanToken.Get All AcsToken in DB: currentTokenAlls.Count=" + currentTokenAlls.Count + "=>End");

                    ProcessTokenNotExistsInDBIntoRam(currentTokenAlls);

                    Inventec.Common.Logging.LogSystem.Info("ScanToken.Goi dong bo token voi cac acs khac neu dung nhieu acs de chạy load balancing=>Begin");
                    TokenManager tokenManager = new TokenManager();
                    var tokenAlls = (from m in currentTokenAlls
                                     where m.IS_ACTIVE == 1
                                     select new AcsCredentialTrackingSDO()
                                     {
                                         ExpireTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(m.EXPIRE_TIME).Value,
                                         LastAccessTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(m.LAST_ACCESS_TIME ?? 0).Value,
                                         LoginAddress = m.LOGIN_ADDRESS,
                                         LoginTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(m.LOGIN_TIME).Value,
                                         MachineName = m.MACHINE_NAME,
                                         RenewCode = m.RENEW_CODE,
                                         TokenCode = m.TOKEN_CODE,
                                         ValidAddress = m.LOGIN_ADDRESS,
                                         VersionApp = m.APP_VERSION,

                                         ApplicationCode = m.APPLICATION_CODE,
                                         Email = m.EMAIL,
                                         GCode = m.GROUP_CODE,
                                         LoginName = m.LOGIN_NAME,
                                         Mobile = m.MOBILE,
                                         UserName = m.USER_NAME,

                                         AuthenticationCode = m.AUTHENTICATION_CODE,
                                         AuthorSystemCode = m.AUTHOR_SYSTEM_CODE

                                     }).ToList();

                    tokenManager.InitThreadSyncData(tokenAlls);
                    Inventec.Common.Logging.LogSystem.Info("ScanToken.Goi dong bo token voi cac acs khac neu dung nhieu acs de chạy load balancing=>End");

                    Inventec.Common.Logging.LogSystem.Info("ScanToken.End");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        void ProcessTokenNotExistsInDBIntoRam(List<ACS_TOKEN> tokenDBs)
        {
            try
            {
                Inventec.Common.Logging.LogSystem.Info("ProcessTokenRemovedInDB.Begin:  remove danh sach token trong RAM cua ACS");
                TokenManager tokenManager = new TokenManager();
                var tokenInRamAlives = tokenManager.GetTokenDataInRamAlives();
                if (tokenInRamAlives != null && tokenInRamAlives.Count > 0)
                {
                    var tokenRemoves = tokenInRamAlives.Where(o => !tokenDBs.Exists(t => t.TOKEN_CODE == o.TokenCode && t.IS_ACTIVE == 1)).ToList();
                    if (tokenRemoves != null && tokenRemoves.Count > 0)
                    {
                        List<string> tokenCodes = tokenRemoves.Select(o => o.TokenCode).ToList();
                        tokenManager.RemoveTokenInRam(tokenCodes);
                    }
                }
                Inventec.Common.Logging.LogSystem.Info("ProcessTokenRemovedInDB.End");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        [Logger]
        public List<ExtraTokenData> GetAlives(string appCode)
        {
            List<ExtraTokenData> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<ACS_TOKEN> resultData = null;
                if (valid)
                {
                    resultData = new AcsTokenGet(param).Get(new AcsTokenFilterQuery() { IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE, ApplicationCode = appCode });
                }
                result = (from m in resultData
                          select new ExtraTokenData()
                          {
                              ExpireTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(m.EXPIRE_TIME).Value,
                              LastAccessTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(m.LAST_ACCESS_TIME ?? 0).Value,
                              LoginAddress = m.LOGIN_ADDRESS,
                              LoginTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(m.LOGIN_TIME).Value,
                              MachineName = m.MACHINE_NAME,
                              RenewCode = m.RENEW_CODE,
                              TokenCode = m.TOKEN_CODE,
                              ValidAddress = m.LOGIN_ADDRESS,
                              VersionApp = m.APP_VERSION,
                              AuthorSystemCode = m.AUTHOR_SYSTEM_CODE,
                              AuthenticationCode = m.AUTHENTICATION_CODE,
                              User = new Inventec.Token.Core.UserData()
                              {
                                  ApplicationCode = m.APPLICATION_CODE,
                                  Email = m.EMAIL,
                                  GCode = m.GROUP_CODE,
                                  LoginName = m.LOGIN_NAME,
                                  Mobile = m.MOBILE,
                                  UserName = m.USER_NAME
                              }
                          }).ToList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        [Logger]
        public List<ExtraTokenData> GetUnAlives(string appCode)
        {
            List<ExtraTokenData> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<ACS_TOKEN> resultData = null;
                if (valid)
                {
                    resultData = new AcsTokenGet(param).Get(new AcsTokenFilterQuery() { IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__FALSE, ApplicationCode = appCode });
                }
                result = (from m in resultData
                          select new ExtraTokenData()
                          {
                              ExpireTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(m.EXPIRE_TIME).Value,
                              LastAccessTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(m.LAST_ACCESS_TIME ?? 0).Value,
                              LoginAddress = m.LOGIN_ADDRESS,
                              LoginTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(m.LOGIN_TIME).Value,
                              MachineName = m.MACHINE_NAME,
                              RenewCode = m.RENEW_CODE,
                              TokenCode = m.TOKEN_CODE,
                              ValidAddress = m.LOGIN_ADDRESS,
                              VersionApp = m.APP_VERSION,
                              AuthorSystemCode = m.AUTHOR_SYSTEM_CODE,
                              AuthenticationCode = m.AUTHENTICATION_CODE,
                              User = new Inventec.Token.Core.UserData()
                              {
                                  ApplicationCode = m.APPLICATION_CODE,
                                  Email = m.EMAIL,
                                  GCode = m.GROUP_CODE,
                                  LoginName = m.LOGIN_NAME,
                                  Mobile = m.MOBILE,
                                  UserName = m.USER_NAME
                              }
                          }).ToList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        [Logger]
        public bool UpdateAlive(List<Inventec.Token.Core.TokenData> tokens)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(tokens);
                if (valid)
                {
                    List<ACS_TOKEN> listUpdates = new List<ACS_TOKEN>();
                    foreach (var item in tokens)
                    {
                        ACS_TOKEN tokenUpdate = new AcsTokenGet(param).Get(new AcsTokenFilterQuery() { IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE, TokenCode = item.TokenCode }).FirstOrDefault();

                        if (IsNotNull(tokenUpdate))
                        {
                            tokenUpdate.LAST_ACCESS_TIME = Convert.ToInt64(item.LastAccessTime.ToString("yyyyMMddHHmmss"));
                            listUpdates.Add(tokenUpdate);
                        }
                    }

                    if (!new AcsTokenUpdate(param).UpdateList(listUpdates))
                    {
                        Inventec.Common.Logging.LogSystem.Debug("Update lastaccesstime for token fail____Get top 10 token in list" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listUpdates), listUpdates.Skip(0).Take(10)));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }


        [Logger]
        public bool UpdateExpireTime(long expireTime)
        {
            bool result = false;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);              
                if (valid)
                {
                    result = new AcsTokenUpdateExpireTime(param).UpdateExpireTime(expireTime);
                }
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
