using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceMachine
{
    partial class HisServiceMachineGet : EntityBase
    {
        public V_HIS_SERVICE_MACHINE GetViewByCode(string code, HisServiceMachineSO search)
        {
            V_HIS_SERVICE_MACHINE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SERVICE_MACHINE.AsQueryable().Where(p => p.SERVICE_MACHINE_CODE == code);
                        if (search.listVHisServiceMachineExpression != null && search.listVHisServiceMachineExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisServiceMachineExpression)
                            {
                                query = query.Where(item);
                            }
                        }
                        result = query.SingleOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => code), code) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
