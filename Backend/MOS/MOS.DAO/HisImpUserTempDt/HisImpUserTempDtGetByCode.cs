using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpUserTempDt
{
    partial class HisImpUserTempDtGet : EntityBase
    {
        public HIS_IMP_USER_TEMP_DT GetByCode(string code, HisImpUserTempDtSO search)
        {
            HIS_IMP_USER_TEMP_DT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_IMP_USER_TEMP_DT.AsQueryable().Where(p => p.IMP_USER_TEMP_DT_CODE == code);
                        if (search.listHisImpUserTempDtExpression != null && search.listHisImpUserTempDtExpression.Count > 0)
                        {
                            foreach (var item in search.listHisImpUserTempDtExpression)
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
