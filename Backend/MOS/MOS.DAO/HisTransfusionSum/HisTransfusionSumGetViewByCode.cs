using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransfusionSum
{
    partial class HisTransfusionSumGet : EntityBase
    {
        public V_HIS_TRANSFUSION_SUM GetViewByCode(string code, HisTransfusionSumSO search)
        {
            V_HIS_TRANSFUSION_SUM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_TRANSFUSION_SUM.AsQueryable().Where(p => p.TRANSFUSION_SUM_CODE == code);
                        if (search.listVHisTransfusionSumExpression != null && search.listVHisTransfusionSumExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisTransfusionSumExpression)
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
