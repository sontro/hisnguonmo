using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentEndTypeExt
{
    partial class HisTreatmentEndTypeExtGet : EntityBase
    {
        public V_HIS_TREATMENT_END_TYPE_EXT GetViewById(long id, HisTreatmentEndTypeExtSO search)
        {
            V_HIS_TREATMENT_END_TYPE_EXT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_TREATMENT_END_TYPE_EXT.AsQueryable().Where(p => p.ID == id);
                        if (search.listVHisTreatmentEndTypeExtExpression != null && search.listVHisTreatmentEndTypeExtExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisTreatmentEndTypeExtExpression)
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
