using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisOtherPaySource
{
    partial class HisOtherPaySourceGet : EntityBase
    {
        public V_HIS_OTHER_PAY_SOURCE GetViewByCode(string code, HisOtherPaySourceSO search)
        {
            V_HIS_OTHER_PAY_SOURCE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_OTHER_PAY_SOURCE.AsQueryable().Where(p => p.OTHER_PAY_SOURCE_CODE == code);
                        if (search.listVHisOtherPaySourceExpression != null && search.listVHisOtherPaySourceExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisOtherPaySourceExpression)
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
