using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpSource
{
    partial class HisImpSourceGet : EntityBase
    {
        public V_HIS_IMP_SOURCE GetViewByCode(string code, HisImpSourceSO search)
        {
            V_HIS_IMP_SOURCE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_IMP_SOURCE.AsQueryable().Where(p => p.IMP_SOURCE_CODE == code);
                        if (search.listVHisImpSourceExpression != null && search.listVHisImpSourceExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisImpSourceExpression)
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
