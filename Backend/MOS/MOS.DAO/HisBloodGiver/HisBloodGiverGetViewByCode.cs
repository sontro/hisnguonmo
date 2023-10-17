using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodGiver
{
    partial class HisBloodGiverGet : EntityBase
    {
        public V_HIS_BLOOD_GIVER GetViewByCode(string code, HisBloodGiverSO search)
        {
            V_HIS_BLOOD_GIVER result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_BLOOD_GIVER.AsQueryable().Where(p => p.BLOOD_GIVER_CODE == code);
                        if (search.listVHisBloodGiverExpression != null && search.listVHisBloodGiverExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisBloodGiverExpression)
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
