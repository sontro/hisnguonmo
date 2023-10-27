using ACS.EFMODEL.DataModels;
using ACS.LibraryEventLog;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsRoleBase;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.EventLogUtil;
using ACS.SDO;
using AutoMapper;
using Inventec.Core;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using ACS.MANAGER.Core.AcsRole.Get;

namespace ACS.MANAGER.Core.AcsRole.RoleBase.Make
{
    class AcsRoleRoleBaseMakeBehavior : BeanObjectBase, IAcsRoleRoleBaseMake
    {
        private List<ACS_ROLE_BASE> recentRoleBases;
        private List<string> roleBases;
        private ACS_ROLE recentRole;
        AcsRoleSDO entity;

        internal AcsRoleRoleBaseMakeBehavior(CommonParam param, AcsRoleSDO data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleRoleBaseMake.Run()
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
                    CreateEventLog();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
                RollbackData();
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
                if (!roleBO.Create(acsRoleDTO))
                {
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsRole_CapNhatThatBai);
                    throw new Exception("Them moi thong tin role that bai. Du lieu se bi rollback" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
                }
            }
            this.recentRole = acsRoleDTO;
        }

        private void ProcessAcsRoleBases(AcsRoleSDO data)
        {
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

                var roleBaseIds = this.recentRoleBases.Select(o => o.ROLE_BASE_ID).ToList();
                var rs = new AcsRoleBO().Get<List<ACS_ROLE>>(new AcsRoleFilterQuery() { IDs = roleBaseIds });
                if (rs != null && rs.Count > 0)
                {
                    roleBases = rs.Select(o => o.ROLE_CODE + " - " + o.ROLE_NAME).ToList();
                }
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
                            rb.ROLE_ID = recentRole.ID;
                            rb.ROLE_BASE_ID = roleId;
                            list.Add(rb);
                        }
                    }
                }
            }

            return list;
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
                if (!new AcsRoleBO().Delete(this.recentRole))
                {
                    Inventec.Common.Logging.LogSystem.Warn("Rollback thong tin AcsRole that bai. Can kiem tra lai log.");
                    result = false;
                }
            }

            return result;
        }

        void CreateEventLog()
        {
            try
            {
                RoleBaseData roleListData = new RoleBaseData();
                roleListData.RoleBaseCodes = roleBases;
                roleListData.RoleCode = entity.ROLE_CODE;
                roleListData.RoleName = entity.ROLE_NAME;
                new EventLogGenerator(EventLog.Enum.AcsRole_SuaVaiTroKeThua)                        
                          .RoleBaseData(roleListData).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
