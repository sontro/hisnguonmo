using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientObservation
{
    partial class HisPatientObservationGet : EntityBase
    {
        public V_HIS_PATIENT_OBSERVATION GetViewByCode(string code, HisPatientObservationSO search)
        {
            V_HIS_PATIENT_OBSERVATION result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_PATIENT_OBSERVATION.AsQueryable().Where(p => p.PATIENT_OBSERVATION_CODE == code);
                        if (search.listVHisPatientObservationExpression != null && search.listVHisPatientObservationExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisPatientObservationExpression)
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
