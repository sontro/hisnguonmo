using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAccidentHelmet
{
    partial class HisAccidentHelmetGet : EntityBase
    {
        public V_HIS_ACCIDENT_HELMET GetViewByCode(string code, HisAccidentHelmetSO search)
        {
            V_HIS_ACCIDENT_HELMET result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_ACCIDENT_HELMET.AsQueryable().Where(p => p.ACCIDENT_HELMET_CODE == code);
                        if (search.listVHisAccidentHelmetExpression != null && search.listVHisAccidentHelmetExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisAccidentHelmetExpression)
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
