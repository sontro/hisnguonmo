using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisContactPoint
{
    partial class HisContactPointGet : EntityBase
    {
        public V_HIS_CONTACT_POINT GetViewByCode(string code, HisContactPointSO search)
        {
            V_HIS_CONTACT_POINT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_CONTACT_POINT.AsQueryable().Where(p => p.CONTACT_POINT_CODE == code);
                        if (search.listVHisContactPointExpression != null && search.listVHisContactPointExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisContactPointExpression)
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
