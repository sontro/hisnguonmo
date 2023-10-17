using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInfusionSum
{
    partial class HisInfusionSumGet : EntityBase
    {
        public HIS_INFUSION_SUM GetByCode(string code, HisInfusionSumSO search)
        {
            HIS_INFUSION_SUM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_INFUSION_SUM.AsQueryable().Where(p => p.INFUSION_SUM_CODE == code);
                        if (search.listHisInfusionSumExpression != null && search.listHisInfusionSumExpression.Count > 0)
                        {
                            foreach (var item in search.listHisInfusionSumExpression)
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
