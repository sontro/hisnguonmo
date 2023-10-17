using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpUserTemp
{
    partial class HisImpUserTempGet : EntityBase
    {
        public V_HIS_IMP_USER_TEMP GetViewByCode(string code, HisImpUserTempSO search)
        {
            V_HIS_IMP_USER_TEMP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_IMP_USER_TEMP.AsQueryable().Where(p => p.IMP_USER_TEMP_CODE == code);
                        if (search.listVHisImpUserTempExpression != null && search.listVHisImpUserTempExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisImpUserTempExpression)
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
