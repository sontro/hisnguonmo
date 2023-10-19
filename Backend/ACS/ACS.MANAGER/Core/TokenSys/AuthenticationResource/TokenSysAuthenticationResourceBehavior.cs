using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsApplication;
using ACS.MANAGER.Core.AcsApplicationRole;
using ACS.MANAGER.Core.AcsApplicationRole.Get;
using ACS.MANAGER.Core.AcsRoleBase;
using ACS.MANAGER.Core.AcsRoleBase.Get;
using ACS.MANAGER.Core.AcsRoleUser;
using ACS.MANAGER.Core.AcsRoleUser.Get;
using ACS.MANAGER.Core.AcsUser;
using ACS.MANAGER.Core.AcsUser.Get;
using ACS.MANAGER.Core.Check;
using AutoMapper;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.TokenSys.Authentication
{
    class TokenSysAuthenticationResourceBehavior : BeanObjectBase, ITokenSysAuthenticationResource
    {
        TokenSysAuthenticationResourceSDO tokenAuthenticationResourceSDO;

        private ACS_USER recentUser;
        private ACS_APPLICATION recentApplication;
        private List<ACS_APPLICATION_ROLE> recentApplicationRoles;
        private List<ACS_ROLE_USER> recentRoleUsers;
        private List<ACS_ROLE_BASE> recentRoleBases;
        private List<long> recentRoleUserIds;

        internal TokenSysAuthenticationResourceBehavior(CommonParam param, TokenSysAuthenticationResourceSDO _tokenAuthenticationResourceSDO)
            : base(param)
        {
            tokenAuthenticationResourceSDO = _tokenAuthenticationResourceSDO;
        }

        ACS_USER ITokenSysAuthenticationResource.Run()
        {
            ACS_USER result = null;
            try
            {
                if (Check())
                {
                    if (String.IsNullOrEmpty(tokenAuthenticationResourceSDO.ApplicationCode))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsLogin_ApplicationCodeKhongHopLe);
                        throw new Exception("applicationCode truyen vao khong hop le. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => tokenAuthenticationResourceSDO.ApplicationCode), tokenAuthenticationResourceSDO.ApplicationCode));
                    }

                    this.recentApplication = GetCurrentApplication(tokenAuthenticationResourceSDO.ApplicationCode);
                    if (this.recentApplication == null || recentApplication.ID == 0)
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsLogin_ApplicationCodeKhongHopLe);
                        throw new Exception("Khong tim thay ung dung. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => tokenAuthenticationResourceSDO.ApplicationCode), tokenAuthenticationResourceSDO.ApplicationCode));
                    }

                    this.recentUser = GetCurrentUser(tokenAuthenticationResourceSDO.LoginName);
                    if (this.recentUser == null)
                        throw new Exception("User khong hop le. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => this.recentUser), this.recentUser));
                    this.recentUser.PASSWORD = "";

                    //Kiem tra application code co hop le hay khong
                    //this.recentApplication = GetCurrentApplication(tokenAuthenticationResourceSDO.ApplicationCode);
                    //if (this.recentApplication == null)
                    //{
                    //    ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsLogin_ApplicationCodeKhongHopLe);
                    //    throw new Exception("Khong tim thay ung dung. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => tokenAuthenticationResourceSDO.ApplicationCode), tokenAuthenticationResourceSDO.ApplicationCode));
                    //}

                    //Kiem tra nguoi dung co quyen vao ung dung hay khong
                    recentApplicationRoles = GetCurrentApplicationRoles(this.recentApplication.ID);
                    if (recentApplicationRoles == null || recentApplicationRoles.Count == 0)
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsLogin_UngDungKhongDuocGanVaiTro);
                        throw new Exception("Ung dung khong duoc gan cho vai tro nao. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => recentApplication), recentApplication) + "|" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => recentApplicationRoles), recentApplicationRoles));
                    }

                    recentRoleUsers = GetCurrentRoleUsers(recentUser.ID);
                    if (recentRoleUsers == null || recentRoleUsers.Count == 0)
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsLogin_NguoiDungKhongDuocGanVaiTro);
                        throw new Exception("Nguoi dung khong duoc gan vai tro nao. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => this.tokenAuthenticationResourceSDO), this.tokenAuthenticationResourceSDO) + "|" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => recentRoleUsers), recentRoleUsers));
                    }

                    this.recentRoleBases = GetCurrentAllRoleBases();

                    ProcessAuthenticateLogin();
                    result = this.recentUser;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TaiKhoanChuaDuocThietLapQuyenTruyCapUngDung);
            }

            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                if (tokenAuthenticationResourceSDO == null) throw new ArgumentNullException("AcsTokenAuthenticationResourceSDO");

                tokenAuthenticationResourceSDO.LoginName = tokenAuthenticationResourceSDO.LoginName.Trim().ToLower();
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

        private ACS_USER GetCurrentUser(string loginname)
        {
            return new AcsUserBO().Get<ACS_USER>(loginname);
        }

        private List<ACS_ROLE_BASE> GetCurrentAcsRoleBases()
        {
            List<ACS_ROLE_BASE> result = new List<ACS_ROLE_BASE>();
            result = this.recentRoleBases.Where(o => this.recentRoleUserIds.Contains(o.ROLE_ID) && !this.recentRoleUserIds.Contains(o.ROLE_BASE_ID)).ToList();
            if (result != null && result.Count > 0)
            {
                var roles = result.Select(o => o.ROLE_BASE_ID).Distinct().ToList();
                this.recentRoleUserIds.AddRange(roles);
                result.AddRange(GetCurrentAcsRoleBases());
            }
            return result;
        }

        private List<ACS_ROLE_BASE> GetCurrentAllRoleBases()
        {
            List<ACS_ROLE_BASE> result = new List<ACS_ROLE_BASE>();
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

        private void ProcessAuthenticateLogin()
        {
            var appRoleIds = this.recentApplicationRoles.Select(o => o.ROLE_ID).ToList();
            this.recentRoleUserIds = this.recentRoleUsers.Select(o => o.ROLE_ID).Distinct().ToList();
            GetCurrentAcsRoleBases();
            var checkRoles = this.recentApplicationRoles.Where(o => this.recentRoleUserIds.Contains(o.ROLE_ID) && o.APPLICATION_ID == this.recentApplication.ID).ToList();
            if (checkRoles != null && checkRoles.Count > 0)
            {

            }
            else
            {
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsLogin_NguoiDungKhongDuocGanQuyenVaoHeThong);
                throw new Exception("Nguoi dung khong duoc gan quyen vao he thong. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => this.recentRoleUserIds), this.recentRoleUserIds) + "|" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => this.recentApplicationRoles), this.recentApplicationRoles));
            }
        }
    }
}
