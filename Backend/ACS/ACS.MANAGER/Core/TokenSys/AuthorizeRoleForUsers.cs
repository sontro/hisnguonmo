using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsApplication;
using ACS.MANAGER.Core.AcsApplicationRole;
using ACS.MANAGER.Core.AcsApplicationRole.Get;
using ACS.MANAGER.Core.AcsRoleBase;
using ACS.MANAGER.Core.AcsRoleBase.Get;
using ACS.MANAGER.Core.AcsRoleUser;
using ACS.MANAGER.Core.AcsRoleUser.Get;
using ACS.MANAGER.Core.TokenSys.Authentication;
using ACS.SDO;
using AutoMapper;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.Core.TokenSys
{
    class AuthorizeRoleForUsers
    {
        AcsTokenAuthenticationSDO entity;
        CommonParam param;
        private ACS_USER recentUser;
        private ACS_APPLICATION recentApplication;
        private List<ACS_APPLICATION_ROLE> recentApplicationRoles;
        private List<ACS_ROLE_USER> recentRoleUsers;
        private List<ACS_ROLE_BASE> recentRoleBases;
        private List<long> recentRoleUserIds;

        internal AuthorizeRoleForUsers(CommonParam _param, AcsTokenAuthenticationSDO data)
        {
            entity = data;
            this.param = _param;
        }

        internal List<ACS_APPLICATION_ROLE> Run()
        {
            List<ACS_APPLICATION_ROLE> result = null;
            try
            {
                if (Check())
                {
                    this.recentUser = Mapper.Map<ACS_USER, ACS_USER>(entity);

                    if (String.IsNullOrEmpty(entity.ApplicationCode))
                    {
                        throw new Exception("applicationCode truyen vao khong hop le. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity.ApplicationCode), entity.ApplicationCode));
                    }

                    this.recentApplication = this.GetCurrentApplication(entity.ApplicationCode);
                    if (this.recentApplication == null || recentApplication.ID == 0)
                    {
                        throw new Exception("Khong tim thay ung dung. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity.ApplicationCode), entity.ApplicationCode));
                    }

                    //Kiểm tra trường cấu hình có check phiên bản phần mềm client tối thiểu để tương thích với phiên bản backend hay không? nếu IS_CHECK_VERSION == 1 => check version client gửi lên phải >= ALLOW_VERSION thì mới có thể đăng nhập được, nếu nhỏ hơn thì đưa ra thông báo cần nâng cấp.
                    if (this.recentApplication.IS_CHECK_VERSION == 1 && !String.IsNullOrEmpty(this.recentApplication.ALLOW_VERSION))
                    {
                        if (!ProcessCheckVersion(entity.AppVersion, this.recentApplication.ALLOW_VERSION))
                        {
                            throw new AllowVersionException("AppVersion");
                        }
                    }

                    if (this.recentUser == null)
                        throw new Exception("User khong hop le. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => this.recentUser), this.recentUser));
                    this.recentUser.PASSWORD = "";

                    //Kiem tra nguoi dung co quyen vao ung dung hay khong
                    this.recentApplicationRoles = this.GetCurrentApplicationRoles(this.recentApplication.ID);
                    if (this.recentApplicationRoles == null || this.recentApplicationRoles.Count == 0)
                    {
                        throw new Exception("Ung dung khong duoc gan cho vai tro nao. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => recentApplication), recentApplication) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => recentApplicationRoles), recentApplicationRoles));
                    }

                    this.recentRoleUsers = this.GetCurrentRoleUsers(recentUser.ID);
                    if (this.recentRoleUsers == null || this.recentRoleUsers.Count == 0)
                    {
                        throw new Exception("Nguoi dung khong duoc gan vai tro nao. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => recentRoleUsers), recentRoleUsers));
                    }

                    this.recentRoleBases = this.GetCurrentAllRoleBases();

                    result = this.ProcessAuthenticateLogin();
                }
            }
            catch (AllowVersionException ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = null;
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsToken_PhienBanHienTaiDaCuPhaiNenPhienBanMoiHonDeHeThongTuongThichChayOnDinh);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = null;
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TaiKhoanChuaDuocThietLapQuyenTruyCapUngDung);
            }

            return result;
        }

        internal List<long> RunWithoutApplicationRole()
        {
            List<long> result = null;
            try
            {
                if (Check())
                {
                    this.recentUser = Mapper.Map<ACS_USER, ACS_USER>(entity);

                    if (String.IsNullOrEmpty(entity.ApplicationCode))
                    {
                        throw new Exception("applicationCode truyen vao khong hop le. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity.ApplicationCode), entity.ApplicationCode));
                    }

                    this.recentApplication = this.GetCurrentApplication(entity.ApplicationCode);
                    if (this.recentApplication == null || recentApplication.ID == 0)
                    {
                        throw new Exception("Khong tim thay ung dung. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity.ApplicationCode), entity.ApplicationCode));
                    }

                    //Kiểm tra trường cấu hình có check phiên bản phần mềm client tối thiểu để tương thích với phiên bản backend hay không? nếu IS_CHECK_VERSION == 1 => check version client gửi lên phải >= ALLOW_VERSION thì mới có thể đăng nhập được, nếu nhỏ hơn thì đưa ra thông báo cần nâng cấp.
                    if (this.recentApplication.IS_CHECK_VERSION == 1 && !String.IsNullOrEmpty(this.recentApplication.ALLOW_VERSION))
                    {
                        if (!ProcessCheckVersion(entity.AppVersion, this.recentApplication.ALLOW_VERSION))
                        {
                            throw new AllowVersionException("AppVersion");
                        }
                    }

                    if (this.recentUser == null)
                        throw new Exception("User khong hop le. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => this.recentUser), this.recentUser));
                    this.recentUser.PASSWORD = "";

                    this.recentRoleUsers = this.GetCurrentRoleUsers(recentUser.ID);
                    if (this.recentRoleUsers == null || this.recentRoleUsers.Count == 0)
                    {
                        throw new Exception("Nguoi dung khong duoc gan vai tro nao. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => recentRoleUsers), recentRoleUsers));
                    }

                    this.recentRoleBases = this.GetCurrentAllRoleBases();

                    result = this.ProcessAuthorizeRoleIds();
                }
            }
            catch (AllowVersionException ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = null;
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsToken_PhienBanHienTaiDaCuPhaiNenPhienBanMoiHonDeHeThongTuongThichChayOnDinh);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = null;
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TaiKhoanChuaDuocThietLapQuyenTruyCapUngDung);
            }

            return result;
        }

        private bool ProcessCheckVersion(string clientVersion, string serverVersion)
        {
            bool valid = true;
            try
            {
                if (String.IsNullOrEmpty(clientVersion)) return true;
                if (String.IsNullOrEmpty(serverVersion)) throw new NullReferenceException("serverVersion is null");
                string[] separators = new string[] { "." };

                var arrClient = clientVersion.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (arrClient == null || arrClient.Count() < 1) throw new NullReferenceException("clientVersion is not valid");

                var arrServer = serverVersion.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (arrServer == null || arrServer.Count() < 1) throw new NullReferenceException("serverVersion is not valid");

                valid = (
                    (clientVersion == serverVersion) ||
                    (clientVersion != serverVersion && serverVersion.Contains(clientVersion)) ||
                    Convert.ToInt32(arrServer[0].Trim()) < Convert.ToInt32(arrClient[0].Trim())
                    ||
                    (Convert.ToInt32(arrServer[0].Trim()) == Convert.ToInt32(arrClient[0].Trim())
                    && (Convert.ToInt32(arrServer[1].Trim()) < Convert.ToInt32(arrClient[1].Trim())
                        || (Convert.ToInt32(arrServer[1].Trim()) == Convert.ToInt32(arrClient[1].Trim())
                        && ((arrServer.Count() > 2 ? Convert.ToInt32(arrServer[2].Trim()) : 0) < (arrClient.Count() > 2 ? Convert.ToInt32(arrClient[2].Trim()) : 0)
                                || ((arrServer.Count() > 2 ? Convert.ToInt32(arrServer[2].Trim()) : 0) == (arrClient.Count() > 2 ? Convert.ToInt32(arrClient[2].Trim()) : 0)
                                && ((arrServer.Count() > 3 ? Convert.ToInt32(arrServer[3].Trim()) : 0) < (arrClient.Count() > 3 ? Convert.ToInt32(arrClient[3].Trim()) : 0)
                                        || ((arrServer.Count() > 3 ? Convert.ToInt32(arrServer[3].Trim()) : 0) == (arrClient.Count() > 3 ? Convert.ToInt32(arrClient[3].Trim()) : 0)
                                            && ((arrServer.Count() > 4 ? Convert.ToInt32(arrServer[4].Trim()) : 0) == (arrClient.Count() > 4 ? Convert.ToInt32(arrClient[4].Trim()) : 0))
                                            )
                                        )
                                    )
                                ))))
                    );
                
                Inventec.Common.Logging.LogSystem.Debug("valid = " + valid);
                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => clientVersion), clientVersion) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => serverVersion), serverVersion));
                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => arrServer), arrServer) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => arrClient), arrClient));
            }
            catch (Exception ex)
            {
                valid = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
        
        private int GetVersionByString(string strVersion)
        {
            try
            {
                return Inventec.Common.TypeConvert.Parse.ToInt32(strVersion.Replace(".", "").Replace("-", "").Replace("_", ""));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return 0;
        }

        private bool Check()
        {
            bool result = true;
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private List<ACS_ROLE_USER> GetCurrentRoleUsers(long userId)
        {
            AcsRoleUserFilterQuery filter = new AcsRoleUserFilterQuery();
            filter.USER_ID = userId;
            return new AcsRoleUserBO().Get<List<ACS_ROLE_USER>>(filter);
        }

        private List<ACS_ROLE_BASE> GetCurrentAcsRoleBases()
        {
            List<ACS_ROLE_BASE> result = new List<ACS_ROLE_BASE>();
            result = this.recentRoleBases.Where(o =>
                this.recentRoleUserIds.Contains(o.ROLE_ID)
                && !this.recentRoleUserIds.Contains(o.ROLE_BASE_ID)).ToList();
            if (result != null && result.Count > 0)
            {
                var roles = result.Select(o => o.ROLE_BASE_ID).Distinct().ToList();
                this.recentRoleUserIds.AddRange(roles);
                result.AddRange(this.GetCurrentAcsRoleBases());
            }
            return result;
        }

        private List<ACS_ROLE_BASE> GetCurrentAllRoleBases()
        {
            AcsRoleBaseFilterQuery filter = new AcsRoleBaseFilterQuery();
            return new AcsRoleBaseBO().Get<List<ACS_ROLE_BASE>>(filter);
        }

        private List<ACS_APPLICATION_ROLE> GetCurrentApplicationRoles(long applicartionId)
        {
            AcsApplicationRoleFilterQuery filter = new AcsApplicationRoleFilterQuery();
            filter.APPLICATION_ID = applicartionId;
            return new AcsApplicationRoleBO().Get<List<ACS_APPLICATION_ROLE>>(filter);
        }

        private ACS_APPLICATION GetCurrentApplication(string applicationCode)
        {
            return new AcsApplicationBO().Get<ACS_APPLICATION>(applicationCode);
        }

        private List<ACS_APPLICATION_ROLE> ProcessAuthenticateLogin()
        {
            var appRoleIds = this.recentApplicationRoles.Select(o => o.ROLE_ID).ToList();
            this.recentRoleUserIds = this.recentRoleUsers.Select(o => o.ROLE_ID).Distinct().ToList();
            this.GetCurrentAcsRoleBases();
            var checkRoles = this.recentApplicationRoles.Where(o =>
                this.recentRoleUserIds.Contains(o.ROLE_ID)
                && o.APPLICATION_ID == this.recentApplication.ID).ToList();
            if (checkRoles == null || checkRoles.Count == 0)
            {
                throw new Exception("Nguoi dung khong duoc gan quyen vao he thong. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => this.recentRoleUserIds), this.recentRoleUserIds) + "|" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => this.recentApplicationRoles), this.recentApplicationRoles));
            }

            return checkRoles;
        }

        private List<long> ProcessAuthorizeRoleIds()
        {
            this.recentRoleUserIds = this.recentRoleUsers.Select(o => o.ROLE_ID).Distinct().ToList();
            this.GetCurrentAcsRoleBases();
            if (recentRoleUserIds == null || recentRoleUserIds.Count == 0)
            {
                throw new Exception("Nguoi dung khong duoc gan quyen vao he thong. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => this.recentRoleUserIds), this.recentRoleUserIds));
            }

            return recentRoleUserIds;
        }

        class AllowVersionException : Exception
        {
            public AllowVersionException() : base() { }
            public AllowVersionException(string message) : base(message) { }
        }
    }
}
