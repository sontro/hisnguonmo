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

namespace ACS.MANAGER.Core.AcsRole.RoleBase.Update
{
    class AcsRoleRoleBaseUpdateBehavior : BeanObjectBase, IAcsRoleRoleBaseUpdate
    {
        private List<ACS_ROLE_BASE> recentRoleBases;
        private ACS_ROLE recentRole;

        AcsRoleSDO entity;

        internal AcsRoleRoleBaseUpdateBehavior(CommonParam param, AcsRoleSDO data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleRoleBaseUpdate.Run()
        {
            bool result = false;
            try
            {
                if (Check())
                {
                    //Tao moi EvAcsRole
                    this.ProcessAcsRole(entity);

                    //Xu ly du lieu detail
                    this.ProcessAcsRoleBases(entity);

                    result = true;
                }
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

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && new AcsRoleCheckVerifyValidReferenceData().Verify(param, entity);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessAcsRole(AcsRoleSDO data)
        {
            ACS_ROLE acsRoleDTO = Mapper.Map<AcsRoleSDO, ACS_ROLE>(data);
            if (acsRoleDTO != null)
            {
                AcsRoleBO roleBO = new AcsRoleBO();
                if (!roleBO.Update(acsRoleDTO))
                {
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsRole_CapNhatThatBai);
                    throw new Exception("Sua thong tin role that bai. Du lieu se bi rollback" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
                }
            }
            this.recentRole = acsRoleDTO;
        }

        private void ProcessAcsRoleBases(AcsRoleSDO data)
        {
            List<ACS_ROLE_BASE> roleBaseOld = GetOldEvAcsRoleBaseDTO(this.recentRole.ID);
            if (roleBaseOld != null && roleBaseOld.Count > 0)
            {
                if (!new AcsRoleBaseBO().Delete(roleBaseOld))
                {
                    throw new Exception("Xoa thong tin AcsRoleBaseDTOs da duoc gan cho role (ROLE_ID = " + this.recentRole.ID + ") that bai.");
                }
            }
            List<ACS_ROLE_BASE> acsRoleBaseDTOs = new List<ACS_ROLE_BASE>();
            acsRoleBaseDTOs = GetEvAcsRoleBaseDTOs(data);
            if (acsRoleBaseDTOs != null && acsRoleBaseDTOs.Count > 0)
            {
                bool result = new AcsRoleBaseBO().Create(acsRoleBaseDTOs);
                if (!result)
                {
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsRoleBase_ThemMoiThatBai);
                    throw new Exception("Them moi thong tin AcsRoleBaseDTOs that bai. Du lieu se bi rollback" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => acsRoleBaseDTOs), acsRoleBaseDTOs));
                }
                this.recentRoleBases = acsRoleBaseDTOs;
            }
        }

        private List<ACS_ROLE_BASE> GetEvAcsRoleBaseDTOs(AcsRoleSDO data)
        {
            bool valid = true;
            List<ACS_ROLE_BASE> list = new List<ACS_ROLE_BASE>();

            valid = valid && IsNotNull(data);
            valid = valid && IsNotNullOrEmpty(data.ROLE_BASE_ID);
            if (valid)
            {
                string[] lines = Regex.Split(data.ROLE_BASE_ID, ",");
                if (lines != null && lines.Length > 0)
                {
                    foreach (var item in lines)
                    {
                        long roleId = Inventec.Common.TypeConvert.Parse.ToInt64(item);
                        if (roleId > 0)
                        {
                            ACS_ROLE_BASE rb = new ACS_ROLE_BASE();
                            rb.ROLE_ID = this.recentRole.ID;
                            rb.ROLE_BASE_ID = roleId;
                            list.Add(rb);
                        }
                    }
                }
            }

            return list;
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
            //rollback du lieu role 
            if (this.recentRole != null)
            {
                if (!new AcsRoleBO().Update(this.recentRole))
                {
                    Inventec.Common.Logging.LogSystem.Error("Rollback thong tin AcsRole that bai. Can kiem tra lai log.");
                    result = false;
                }
            }

            return result;
        }
    }
}
