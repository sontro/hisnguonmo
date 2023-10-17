using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskAccess
{
    partial class HisKskAccessGet : EntityBase
    {
        public V_HIS_KSK_ACCESS GetViewByCode(string code, HisKskAccessSO search)
        {
            V_HIS_KSK_ACCESS result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_KSK_ACCESS.AsQueryable().Where(p => p.KSK_ACCESS_CODE == code);
                        if (search.listVHisKskAccessExpression != null && search.listVHisKskAccessExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisKskAccessExpression)
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
