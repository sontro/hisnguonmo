using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentResult
{
    partial class HisTreatmentResultGet : EntityBase
    {
        public V_HIS_TREATMENT_RESULT GetViewByCode(string code, HisTreatmentResultSO search)
        {
            V_HIS_TREATMENT_RESULT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_TREATMENT_RESULT.AsQueryable().Where(p => p.TREATMENT_RESULT_CODE == code);
                        if (search.listVHisTreatmentResultExpression != null && search.listVHisTreatmentResultExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisTreatmentResultExpression)
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
