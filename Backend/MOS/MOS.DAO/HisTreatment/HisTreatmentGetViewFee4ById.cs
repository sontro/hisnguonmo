using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatment
{
    partial class HisTreatmentGet : EntityBase
    {
        public V_HIS_TREATMENT_FEE_4 GetFeeView4ById(long id, HisTreatmentSO search)
        {
            V_HIS_TREATMENT_FEE_4 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_TREATMENT_FEE_4.AsQueryable().Where(p => p.ID == id);
                        if (search.listVHisTreatmentFee4Expression != null && search.listVHisTreatmentFee4Expression.Count > 0)
                        {
                            foreach (var item in search.listVHisTreatmentFee4Expression)
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