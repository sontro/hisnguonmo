using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCoTreatment
{
    partial class HisCoTreatmentGet : EntityBase
    {
        public HIS_CO_TREATMENT GetByCode(string code, HisCoTreatmentSO search)
        {
            HIS_CO_TREATMENT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_CO_TREATMENT.AsQueryable().Where(p => p.CO_TREATMENT_CODE == code);
                        if (search.listHisCoTreatmentExpression != null && search.listHisCoTreatmentExpression.Count > 0)
                        {
                            foreach (var item in search.listHisCoTreatmentExpression)
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
