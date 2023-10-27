using ACS.EFMODEL.DataModels;
using ACS.LibraryEventLog;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsApplication;
using ACS.MANAGER.Core.AcsApplicationRole.Get;
using ACS.MANAGER.Core.AcsRole;
using ACS.MANAGER.Core.AcsRole.Get;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.EventLogUtil;
using Inventec.Core;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.AcsApplicationRole.Delete
{
    class AcsApplicationRoleDeleteBehaviorListEv : BeanObjectBase, IAcsApplicationRoleDelete
    {
        List<ACS_APPLICATION_ROLE> listDeletes;
        List<long> entity;
        List<string> roleNames;
        string applicationCode;
        string applicationName;

        internal AcsApplicationRoleDeleteBehaviorListEv(CommonParam param, List<long> data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsApplicationRoleDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsApplicationRoleDAO.TruncateList(listDeletes);
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
                    result = result && AcsApplicationRoleCheckVerifyIsUnlock.Verify(param, item);
                }

                AcsApplicationRoleFilterQuery moduleRoleFilterQuery = new AcsApplicationRoleFilterQuery();
                moduleRoleFilterQuery.IDs = entity;
                this.listDeletes = DAOWorker.AcsApplicationRoleDAO.Get(moduleRoleFilterQuery.Query(), param);
                result = result && (this.listDeletes != null && this.listDeletes.Count > 0);
                if (!result || this.listDeletes == null || this.listDeletes.Count == 0)
                {
                    Inventec.Common.Logging.LogSystem.Info("Kiem tra dieu kien truoc khi xoa ApplicationRole tra ve khong hop le.____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listDeletes), listDeletes));
                }

                var app = (new AcsApplicationBO().Get<ACS_APPLICATION>(listDeletes.First().APPLICATION_ID) ?? new ACS_APPLICATION());
                applicationName = app.APPLICATION_NAME;
                applicationCode = app.APPLICATION_CODE;
                roleNames = (new AcsRoleBO().Get<List<ACS_ROLE>>(new AcsRoleFilterQuery() { IDs = listDeletes.Select(o => o.ROLE_ID).ToList() }) ?? new List<ACS_ROLE>()).Select(o => o.ROLE_NAME).ToList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        void CreateEventLog()
        {
            try
            {
                ApplicationRoleData applicationRoleData = new ApplicationRoleData();
                applicationRoleData.ApplicationCode = applicationCode;
                applicationRoleData.RoleNames = roleNames;

                new EventLogGenerator(EventLog.Enum.AcsApplicationRole_XoaQuyenTruyCap).ApplicationRoleData(applicationRoleData).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
