using ACS.EFMODEL.DataModels;
using ACS.MANAGER.AcsAuthenRequest;
using ACS.MANAGER.AcsToken;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsUser;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using AutoMapper;
using Inventec.Core;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using ACS.LibraryConfig;
using System.Threading.Tasks;
using Inventec.Token.Core;
using ACS.MANAGER.Core.AcsRole;
using ACS.MANAGER.Core.AcsRole.Get;
using Inventec.Token.AuthSystem;

namespace ACS.MANAGER.Core.TokenSys.LoginByAuthenRequest
{
    class LoginByAuthenRequestBehavior : BeanObjectBase, ILoginByAuthenRequest
    {
        LoginByAuthenRequestTDO entity;
        ACS_TOKEN currentToken = null;
        ACS_AUTHEN_REQUEST currentAuthenRequest;
        ACS_USER user;

        internal LoginByAuthenRequestBehavior(CommonParam param, LoginByAuthenRequestTDO data)
            : base(param)
        {
            entity = data;
        }

        /// <summary>
        /// Bổ sung api cấp token theo yêu cầu xác thực:
        ///- Các ứng dụng sẽ gọi vào api này.
        ///- Không cần xác thực.
        ///- Input:
        ///+ Mã xác thực.
        ///+ Mã hệ thống ủy quyền.
        ///+ Mã ứng dụng cần đăng nhập.
        ///
        ///- Xử lý:
        ///+ Nếu không tồn tại yêu cầu xác thực thỏa mãn: Mã xác thực bằng mã truyền lên (AUTHENTICATION_CODE), Mã hệ thống ủy quyền bằng mã truyền lên (TDL_AUTHOR_SYSTEM_CODE), Chưa được cấp token (IS_ISSUED_TOKEN = NULL), Thời gian hết hạn lớn hơn hoặc bằng thời gian hiện tại, trạng thái mở khóa (IS_ACTIVE = 1) -> Chặn báo lỗi "Yêu cầu xác thực không tồn tại hoặc đã hết hạn".
        ///+ Nếu có nhiều hơn 1 yêu cầu xác thực thỏa mãn thì lấy cái có request time lớn nhất (Các yêu cầu còn lại thì khóa lại (IS_ACTIVE = 0)).
        ///+ Nếu đúng thì thực hiện cấp token cho người dùng.
        ///+ Set Mã xác thực AUTHENTICATION_CODE.
        ///+ Thông tin người yêu cầu xác thực (REQUEST_USERNAME, EMAIL, MOBILE).
        ///+ Tên đăng nhập là thông tin bổ sung và mã hệ thống ủy quyền (AUTHOR_SYSTEM_CODE + "_" + ADDITIONAL_INFO. Trường hợp HIS chính là mã CCHN).
        ///+ Mã hệ thống ủy quyền AUTHOR_SYSTEM_CODE.
        ///+ Mã ứng dụng, hạn token, ...

        ///- Output: Dữ liệu token.
        /// </summary>
        /// <returns></returns>
        Inventec.Token.Core.TokenData ILoginByAuthenRequest.Run()
        {
            Inventec.Token.Core.TokenData result = null;
            bool valid = true;
            try
            {
                valid = valid && Check();
                if (valid)
                {
                    AcsAuthenRequestFilterQuery authenRequestFilterQuery = new AcsAuthenRequest.AcsAuthenRequestFilterQuery();
                    authenRequestFilterQuery.AUTHENTICATION_CODE = entity.AuthenticationCode;
                    authenRequestFilterQuery.AUTHOR_SYSTEM_CODE = entity.AuthorSystemCode;
                    authenRequestFilterQuery.IS_ISSUED_TOKEN = false;
                    authenRequestFilterQuery.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;

                    var authenRequests = new AcsAuthenRequestManager().Get(authenRequestFilterQuery);
                    if (authenRequests != null && authenRequests.Data != null && authenRequests.Data.Count > 0)
                    {
                        currentAuthenRequest = authenRequests.Data.OrderByDescending(o => o.REQUEST_TIME).FirstOrDefault();
                        if (currentAuthenRequest == null)
                        {
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsAuthenRequest_AuthenRequest__YeuCauXacThucKhongTonTaiHoacDaHetHan);
                            valid = false;
                        }
                        if (currentAuthenRequest != null && currentAuthenRequest.REQUEST_TIME.HasValue)
                        {
                            long timenow = Inventec.Common.TypeConvert.Parse.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm"));
                            var dtRequestTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(currentAuthenRequest.REQUEST_TIME.Value);
                            if (Inventec.Common.TypeConvert.Parse.ToInt64(dtRequestTime.Value.AddMinutes(WebConfig.AuthenRequest__Timeout).ToString("yyyyMMddHHmm")) < timenow)
                            {
                                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsAuthenRequest_AuthenRequest__YeuCauXacThucKhongTonTaiHoacDaHetHan);
                                Inventec.Common.Logging.LogSystem.Warn(MessageUtil.GetMessage(LibraryMessage.Message.Enum.Core_AcsAuthenRequest_AuthenRequest__YeuCauXacThucKhongTonTaiHoacDaHetHan) + "____"
                                    + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => WebConfig.AuthenRequest__Timeout), WebConfig.AuthenRequest__Timeout)
                                    + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dtRequestTime), dtRequestTime)
                                    + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => timenow), timenow)
                                    + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => currentAuthenRequest), currentAuthenRequest));
                                valid = false;
                            }
                        }

                        if (valid)
                        {
                            CommonParam commonParam = new Inventec.Core.CommonParam();
                            if (InsertTokenDataToDb(commonParam))
                            {
                                result = GenerateExtraToken(currentToken);

                                List<ACS_AUTHEN_REQUEST> listUpdates = new List<ACS_AUTHEN_REQUEST>();
                                if (authenRequests.Data.Count > 1)
                                {
                                    var authenRequestForUpdates = authenRequests.Data.Where(o => o.ID != currentAuthenRequest.ID).ToList();
                                    if (authenRequestForUpdates != null && authenRequestForUpdates.Count > 0)
                                    {
                                        authenRequestForUpdates.ForEach(t => t.IS_ACTIVE = 0);
                                        //authenRequestForUpdates.ForEach(t => t.IS_ISSUED_TOKEN = 1);
                                        listUpdates.AddRange(authenRequestForUpdates);
                                    }
                                }
                                currentAuthenRequest.IS_ISSUED_TOKEN = 1;
                                listUpdates.Add(currentAuthenRequest);
                                if (listUpdates.Count > 0)
                                {
                                    var updateDataResults = new AcsAuthenRequestManager().UpdateList(listUpdates);
                                    if (updateDataResults == null || updateDataResults.Data == null || updateDataResults.Data.Count == 0)
                                    {
                                        //MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsAuthenRequest_AuthenRequest__KhoaCacYeuCauXacThucThatBai);
                                        Inventec.Common.Logging.LogSystem.Warn(MessageUtil.GetMessage(LibraryMessage.Message.Enum.Core_AcsAuthenRequest_AuthenRequest__KhoaCacYeuCauXacThucThatBai));
                                    }
                                }
                                //Ghi eventLog dang nhap
                                AddActivityLog(currentToken);
                                LogEventLoginBySecretKeySuccess();
                            }
                            else
                            {
                                Inventec.Common.Logging.LogSystem.Warn("Call api LoginByAuthenRequest Authentication thanh cong, tuy nhien InsertTokenDataToDb that bai. Dang nhap that bai. Du lieu dau vao____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
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
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsAuthenRequest_AuthenRequest__YeuCauXacThucKhongTonTaiHoacDaHetHan);
                        Inventec.Common.Logging.LogSystem.Warn("Call api LoginByAuthenRequest that bai, Khong tim thay yeu cau xac thuc theo dieu kien gui len____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                    }
                    return result;
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Warn("Call api LoginByAuthenRequest that bai, du lieu nguoi dung gui len khong hop le____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
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
                bool valid = (AcsUserCheckVerifyValidDataForAuthorize.Verify(param, ref user, currentToken.LOGIN_NAME));
                if (valid)
                {
                    if (valid)
                    {
                        AcsTokenAuthenticationSDO authenticationSDO = new AcsTokenAuthenticationSDO();
                        authenticationSDO = Mapper.Map<ACS_USER, AcsTokenAuthenticationSDO>(user);
                        authenticationSDO.ApplicationCode = currentToken.APPLICATION_CODE;
                        authenticationSDO.AppVersion = currentToken.APP_VERSION;
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
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public bool InsertTokenDataToDb(CommonParam commonParam)
        {
            bool result = false;
            try
            {
                ACS.MANAGER.Token.TokenManager tokenManager = new ACS.MANAGER.Token.TokenManager();
                entity.LOGIN_ADDRESS = String.IsNullOrEmpty(entity.LOGIN_ADDRESS) ? Inventec.Token.ResourceSystem.ResourceTokenManager.GetRequestAddress() : entity.LOGIN_ADDRESS;
                string MachineName = Environment.MachineName;
                ACS_TOKEN entityToken = new ACS_TOKEN();
                entityToken.APP_VERSION = "1.0.0.0";
                entityToken.EMAIL = currentAuthenRequest.EMAIL;
                entityToken.LOGIN_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                entityToken.EXPIRE_TIME = Convert.ToInt64(DateTime.Now.AddMinutes(WebConfig.AuthSystem__TokenTimeout).ToString("yyyyMMddHHmmss"));
                entityToken.LAST_ACCESS_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                entityToken.LOGIN_ADDRESS = entity.LOGIN_ADDRESS;
                entityToken.MACHINE_NAME = MachineName;
                entityToken.MOBILE = currentAuthenRequest.MOBILE;
                entityToken.USER_NAME = currentAuthenRequest.REQUEST_USERNAME;
                entityToken.LOGIN_NAME = String.Format("{0}_{1}", currentAuthenRequest.TDL_AUTHOR_SYSTEM_CODE, currentAuthenRequest.ADDITIONAL_INFO);
                entityToken.APPLICATION_CODE = entity.AppCode;
                entityToken.AUTHENTICATION_CODE = currentAuthenRequest.AUTHENTICATION_CODE;
                entityToken.AUTHOR_SYSTEM_CODE = currentAuthenRequest.TDL_AUTHOR_SYSTEM_CODE;

                //entityToken.TOKEN_CODE = ACS.UTILITY.GenerateToken.GenerateTokenCode(currentAuthenRequest.ADDITIONAL_INFO, entityToken.LOGIN_ADDRESS);
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
                    Inventec.Token.AuthSystem.ExtraTokenData extraTokenData = tokenManager.GenerateExtraToken(currentToken);
                    result = tokenManager.InsertTokenInRam(extraTokenData);
                    tokenManager.InitThreadSyncTokenInsert(extraTokenData);
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug("InsertTokenDataToDb that bai. "
                        + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entityToken), entityToken));
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

                eventLog.Description = String.Format(paramDNTC.GetMessage(), this.entity.AppCode);
                eventLog.EventTime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                eventLog.Ip = Inventec.Token.ResourceSystem.ResourceTokenManager.GetRequestAddress();
                eventLog.LogginName = currentAuthenRequest.ADDITIONAL_INFO;
                eventLog.AppCode = this.entity.AppCode;
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
                if (entity.AuthenticationCode == null) throw new ArgumentNullException("AuthenticationCode");
                if (entity.AuthorSystemCode == null) throw new ArgumentNullException("AuthorSystemCode");
                if (entity.AppCode == null) throw new ArgumentNullException("AppCode");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
