using ACS.EFMODEL.DataModels;
using ACS.LibraryEventLog;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsModule;
using ACS.MANAGER.Core.AcsModule.Get;
using ACS.MANAGER.Core.AcsRole;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.EventLogUtil;
using Inventec.Core;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.AcsModuleRole.Create
{
    class AcsModuleRoleCreateListBehavior : BeanObjectBase, IAcsModuleRoleCreate
    {
        List<ACS_MODULE_ROLE> entities;
        string roleCode;
        string roleName;
        List<string> moduleLinks;

        internal AcsModuleRoleCreateListBehavior(CommonParam param, List<ACS_MODULE_ROLE> data)
            : base(param)
        {
            entities = data;
        }

        bool IAcsModuleRoleCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsModuleRoleDAO.CreateList(entities);
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
                result = result && AcsModuleRoleCheckVerifyValidData.Verify(param, entities);
                var role = (new AcsRoleBO().Get<ACS_ROLE>(entities.First().ROLE_ID) ?? new ACS_ROLE());
                roleCode = role.ROLE_CODE;
                roleName = role.ROLE_NAME;
                moduleLinks = (new AcsModuleBO().Get<List<ACS_MODULE>>(new AcsModuleFilterQuery() { IDs = entities.Select(o => o.MODULE_ID).ToList() }) ?? new List<ACS_MODULE>()).Select(o => (o.MODULE_NAME + " - " + o.MODULE_LINK)).ToList();
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
                ModuleRoleData moduleRoleData = new ModuleRoleData();
                moduleRoleData.RoleCode = roleCode;
                moduleRoleData.RoleName = roleName;
                moduleRoleData.ModuleLinks = moduleLinks;

                new EventLogGenerator(EventLog.Enum.AcsModuleRole_ThemVaiTroChucNang)                          
                          .ModuleRoleData(moduleRoleData).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
