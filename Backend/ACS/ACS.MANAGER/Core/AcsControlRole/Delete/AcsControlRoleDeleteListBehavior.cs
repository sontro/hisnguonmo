using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsControlRole.Get;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsControlRole.Delete
{
    class AcsControlRoleDeleteListBehavior : BeanObjectBase, IAcsControlRoleDelete
    {
        List<ACS_CONTROL_ROLE> listDeletes;
        List<long> entity;

        internal AcsControlRoleDeleteListBehavior(CommonParam param, List<long> data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsControlRoleDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsControlRoleDAO.TruncateList(listDeletes);
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
                    result = result && AcsControlRoleCheckVerifyIsUnlock.Verify(param, item);
                }

                AcsControlRoleFilterQuery moduleRoleFilterQuery = new AcsControlRoleFilterQuery();
                moduleRoleFilterQuery.IDs = entity;
                this.listDeletes = DAOWorker.AcsControlRoleDAO.Get(moduleRoleFilterQuery.Query(), param);
                result = result && (this.listDeletes != null && this.listDeletes.Count > 0);
                if (!result || this.listDeletes == null || this.listDeletes.Count == 0)
                {
                    Inventec.Common.Logging.LogSystem.Info("Kiem tra dieu kien truoc khi xoa ControlRole tra ve khong hop le.____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listDeletes), listDeletes));
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
    }
}
