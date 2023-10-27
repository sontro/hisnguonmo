using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsRoleBase;
using ACS.MANAGER.Core.AcsRoleBase.Get;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using AutoMapper;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ACS.MANAGER.Core.AcsRole.RoleBase.Delete
{
    class AcsRoleRoleBaseDeleteBehavior : BeanObjectBase, IAcsRoleRoleBaseDelete
    {
        private List<ACS_ROLE_BASE> recentEvAcsRoleBaseDTOs;

        ACS_ROLE entity;

        internal AcsRoleRoleBaseDeleteBehavior(CommonParam param, ACS_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleRoleBaseDelete.Run()
        {
            bool result = false;
            try
            {
                //Xu ly du lieu detail
                this.ProcessAcsRoleBases(entity);

                //Tao moi EvAcsRole
                this.ProcessAcsRole(entity);

                result = true;
            }
            catch (Exception ex)
            {
                RollbackData();
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessAcsRole(ACS_ROLE data)
        {
            if (!new AcsRoleBO().Delete(data))
            {
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsRole_XoaThatBai);
                throw new Exception("Xoa thong tin role that bai. Du lieu se bi rollback" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
            }
        }

        private void ProcessAcsRoleBases(ACS_ROLE data)
        {
            recentEvAcsRoleBaseDTOs = GetOldEvAcsRoleBaseDTO(data.ID);
            if (recentEvAcsRoleBaseDTOs != null && recentEvAcsRoleBaseDTOs.Count > 0)
            {
                if (!new AcsRoleBaseBO().Delete(recentEvAcsRoleBaseDTOs))
                {
                    throw new Exception("Xoa thong tin AcsRoleBaseDTOs da duoc gan cho role (ROLE_ID = " + data.ID + ") that bai.");
                }
            }
        }

        private List<ACS_ROLE_BASE> GetOldEvAcsRoleBaseDTO(long roleId)
        {
            AcsRoleBaseFilterQuery filter = new AcsRoleBaseFilterQuery();
            filter.ROLE_ID = roleId;
            return new AcsRoleBaseBO().Get<List<ACS_ROLE_BASE>>(filter);
        }

        /// <summary>
        /// Rollback du lieu Role
        /// </summary>
        /// <returns></returns>
        private bool RollbackData()
        {
            bool result = true;
            //rollback du lieu role base
            if (this.recentEvAcsRoleBaseDTOs != null && recentEvAcsRoleBaseDTOs.Count > 0)
            {
                if (!new AcsRoleBaseBO().Create(this.recentEvAcsRoleBaseDTOs))
                {
                    Inventec.Common.Logging.LogSystem.Error("Rollback thong tin AcsRoleBase that bai. Can kiem tra lai log.");
                    result = false;
                }
            }

            return result;
        }
    }
}
