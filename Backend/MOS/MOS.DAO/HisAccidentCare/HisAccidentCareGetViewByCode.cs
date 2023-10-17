using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAccidentCare
{
    partial class HisAccidentCareGet : EntityBase
    {
        public V_HIS_ACCIDENT_CARE GetViewByCode(string code, HisAccidentCareSO search)
        {
            V_HIS_ACCIDENT_CARE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_ACCIDENT_CARE.AsQueryable().Where(p => p.ACCIDENT_CARE_CODE == code);
                        if (search.listVHisAccidentCareExpression != null && search.listVHisAccidentCareExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisAccidentCareExpression)
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
