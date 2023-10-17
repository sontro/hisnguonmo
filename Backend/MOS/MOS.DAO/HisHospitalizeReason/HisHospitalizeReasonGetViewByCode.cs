using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHospitalizeReason
{
    partial class HisHospitalizeReasonGet : EntityBase
    {
        public V_HIS_HOSPITALIZE_REASON GetViewByCode(string code, HisHospitalizeReasonSO search)
        {
            V_HIS_HOSPITALIZE_REASON result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_HOSPITALIZE_REASON.AsQueryable().Where(p => p.HOSPITALIZE_REASON_CODE == code);
                        if (search.listVHisHospitalizeReasonExpression != null && search.listVHisHospitalizeReasonExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisHospitalizeReasonExpression)
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
