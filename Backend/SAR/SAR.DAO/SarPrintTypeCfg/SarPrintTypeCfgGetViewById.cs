using SAR.DAO.Base;
using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarPrintTypeCfg
{
    partial class SarPrintTypeCfgGet : EntityBase
    {
        public V_SAR_PRINT_TYPE_CFG GetViewById(long id, SarPrintTypeCfgSO search)
        {
            V_SAR_PRINT_TYPE_CFG result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_SAR_PRINT_TYPE_CFG.AsQueryable().Where(p => p.ID == id);
                        if (search.listVSarPrintTypeCfgExpression != null && search.listVSarPrintTypeCfgExpression.Count > 0)
                        {
                            foreach (var item in search.listVSarPrintTypeCfgExpression)
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
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
