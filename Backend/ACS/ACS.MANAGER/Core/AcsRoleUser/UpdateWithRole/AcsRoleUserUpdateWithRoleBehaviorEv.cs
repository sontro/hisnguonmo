using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsRoleUser.Get;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser.UpdateWithRole
{
    class AcsRoleUserUpdateWithRoleBehaviorEv : BeanObjectBase, IAcsRoleUserUpdateWithRole
    {
        private List<ACS_ROLE_USER> recentRoleUsers;
        private ACS_USER recentUser;
        AcsRoleUserForUpdateSDO entity;

        internal AcsRoleUserUpdateWithRoleBehaviorEv(CommonParam param, AcsRoleUserForUpdateSDO data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleUserUpdateWithRole.Run()
        {
            bool result = false;
            try
            {
                if (Check())
                {
                    this.recentUser = Mapper.Map<ACS_USER, ACS_USER>(entity.User);
                    ProcessUpdate(entity);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && AcsRoleUserCheckVerifyValidDataForGetTree.Verify(param, entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private List<ACS_ROLE_USER> GetOldACS_ROLE_USER(long userId)
        {
            AcsRoleUserFilterQuery filter = new AcsRoleUserFilterQuery();
            filter.USER_ID = userId;
            return new AcsRoleUserBO().Get<List<ACS_ROLE_USER>>(filter);
        }

        private void ProcessUpdate(AcsRoleUserForUpdateSDO data)
        {
            this.recentRoleUsers = GetOldACS_ROLE_USER(this.recentUser.ID);
            if (this.recentRoleUsers != null && this.recentRoleUsers.Count > 0)
            {
                if (!new AcsRoleUserBO().Delete(this.recentRoleUsers))
                {
                    throw new Exception("Xoa thong tin AcsRoleUserDTOs da duoc gan cho user (USER_ID = " + this.recentUser.ID + ") that bai.");
                }
            }

            List<ACS_ROLE_USER> acsRoleBaseDTOs = new List<ACS_ROLE_USER>();
            acsRoleBaseDTOs = Mapper.Map<List<ACS_ROLE_USER>>(data.RoleUsers);
            if (acsRoleBaseDTOs != null && acsRoleBaseDTOs.Count > 0)
            {
                bool result = new AcsRoleUserBO().Create(acsRoleBaseDTOs);
                if (!result)
                {
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsRoleUser_ThemMoiThatBai);
                    throw new Exception("Them moi thong tin AcsRoleUserDTOs that bai. Du lieu se bi rollback" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => acsRoleBaseDTOs), acsRoleBaseDTOs));
                }
            }
        }

        /// <summary>
        /// Rollback du lieu Role
        /// </summary>
        /// <returns></returns>
        private bool RollbackData()
        {
            bool result = true;
            //rollback du lieu role 
            if (this.recentRoleUsers != null && recentRoleUsers.Count > 0)
            {
                if (!new AcsRoleUserBO().Create(this.recentRoleUsers))
                {
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsRoleUser_RollBackThatBai);
                    LogSystem.Error("Rollback thong tin AcsRoleUser that bai. Can kiem tra lai log.");
                    result = false;
                }
            }

            return result;
        }
    }
}
