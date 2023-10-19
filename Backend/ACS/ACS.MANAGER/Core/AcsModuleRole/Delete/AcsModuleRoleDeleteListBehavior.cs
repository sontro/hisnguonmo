using ACS.EFMODEL.DataModels;
using ACS.LibraryEventLog;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsModule;
using ACS.MANAGER.Core.AcsModule.Get;
using ACS.MANAGER.Core.AcsModuleRole.Get;
using ACS.MANAGER.Core.AcsRole;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.EventLogUtil;
using Inventec.Core;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.AcsModuleRole.Delete
{
    class AcsModuleRoleDeleteListBehavior : BeanObjectBase, IAcsModuleRoleDelete
    {
        List<long> entity;
        List<ACS_MODULE_ROLE> listDeletes;
        string roleCode;
        string roleName;
        List<string> moduleLinks;


        internal AcsModuleRoleDeleteListBehavior(CommonParam param, List<long> data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleRoleDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsModuleRoleDAO.TruncateList(this.listDeletes);
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
                foreach (var item in entity)
                {
                    result = result && AcsModuleRoleCheckVerifyIsUnlock.Verify(param, item);
                }

                AcsModuleRoleFilterQuery moduleRoleFilterQuery = new AcsModuleRoleFilterQuery();
                moduleRoleFilterQuery.IDs = entity;
                this.listDeletes = DAOWorker.AcsModuleRoleDAO.Get(moduleRoleFilterQuery.Query(), param);
                result = result && (this.listDeletes != null && this.listDeletes.Count > 0);
                if (!result || this.listDeletes == null || this.listDeletes.Count == 0)
                {
                    Inventec.Common.Logging.LogSystem.Info("Kiem tra dieu kien truoc khi xoa ModuleRole tra ve khong hop le.____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listDeletes), listDeletes));
                }

                var role = (new AcsRoleBO().Get<ACS_ROLE>(listDeletes.First().ROLE_ID) ?? new ACS_ROLE());
                roleCode = role.ROLE_CODE;
                roleName = role.ROLE_NAME;
                moduleLinks = (new AcsModuleBO().Get<List<ACS_MODULE>>(new AcsModuleFilterQuery() { IDs = listDeletes.Select(o => o.MODULE_ID).ToList() }) ?? new List<ACS_MODULE>()).Select(o => (o.MODULE_NAME + " - " + o.MODULE_LINK)).ToList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
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

                new EventLogGenerator(EventLog.Enum.AcsModuleRole_XoaVaiTroChucNang)                       
                          .ModuleRoleData(moduleRoleData).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
