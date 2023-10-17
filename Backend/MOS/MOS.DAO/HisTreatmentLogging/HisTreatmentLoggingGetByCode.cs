using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentLogging
{
    partial class HisTreatmentLoggingGet : EntityBase
    {
        public HIS_TREATMENT_LOGGING GetByCode(string code, HisTreatmentLoggingSO search)
        {
            HIS_TREATMENT_LOGGING result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_TREATMENT_LOGGING.AsQueryable().Where(p => p.TREATMENT_LOGGING_CODE == code);
                        if (search.listHisTreatmentLoggingExpression != null && search.listHisTreatmentLoggingExpression.Count > 0)
                        {
                            foreach (var item in search.listHisTreatmentLoggingExpression)
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
