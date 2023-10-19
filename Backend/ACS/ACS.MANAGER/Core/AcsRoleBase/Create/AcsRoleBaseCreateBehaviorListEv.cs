using ACS.EFMODEL.DataModels;
using ACS.LibraryEventLog;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsRole;
using ACS.MANAGER.Core.AcsRole.Get;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.EventLogUtil;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.AcsRoleBase.Create
{
    class AcsRoleBaseCreateBehaviorListEv : BeanObjectBase, IAcsRoleBaseCreate
    {
        List<ACS_ROLE_BASE> entities;
        private List<string> roleBases;
        private ACS_ROLE recentRole;

        internal AcsRoleBaseCreateBehaviorListEv(CommonParam param, List<ACS_ROLE_BASE> datas)
            : base(param)
        {
            entities = datas;
        }

        bool IAcsRoleBaseCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleBaseDAO.CreateList(entities);
                if (result)
                {
                    CreateEventLog();
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
                result = result && AcsRoleBaseCheckVerifyValidData.Verify(param, entities);

                var roleBaseIds = this.entities.Select(o => o.ROLE_BASE_ID).ToList();
                var roleId = this.entities.Select(o => o.ROLE_ID).First();
                var rs = new AcsRoleBO().Get<List<ACS_ROLE>>(new AcsRoleFilterQuery() { IDs = roleBaseIds });
                if (rs != null && rs.Count > 0)
                {
                    roleBases = rs.Select(o => o.ROLE_CODE + " - " + o.ROLE_NAME).ToList();
                }

                recentRole = new AcsRoleBO().Get<ACS_ROLE>(roleId) ?? new ACS_ROLE();
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        void CreateEventLog()
        {
            try
            {
                RoleBaseData roleListData = new RoleBaseData();
                roleListData.RoleBaseCodes = roleBases;
                roleListData.RoleCode = recentRole.ROLE_CODE;
                roleListData.RoleName = recentRole.ROLE_NAME;
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
