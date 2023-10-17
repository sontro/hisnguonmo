using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransfusion
{
    partial class HisTransfusionGet : EntityBase
    {
        public V_HIS_TRANSFUSION GetViewByCode(string code, HisTransfusionSO search)
        {
            V_HIS_TRANSFUSION result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_TRANSFUSION.AsQueryable().Where(p => p.TRANSFUSION_CODE == code);
                        if (search.listVHisTransfusionExpression != null && search.listVHisTransfusionExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisTransfusionExpression)
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
