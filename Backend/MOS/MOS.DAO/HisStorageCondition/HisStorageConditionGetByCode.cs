using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisStorageCondition
{
    partial class HisStorageConditionGet : EntityBase
    {
        public HIS_STORAGE_CONDITION GetByCode(string code, HisStorageConditionSO search)
        {
            HIS_STORAGE_CONDITION result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_STORAGE_CONDITION.AsQueryable().Where(p => p.STORAGE_CONDITION_CODE == code);
                        if (search.listHisStorageConditionExpression != null && search.listHisStorageConditionExpression.Count > 0)
                        {
                            foreach (var item in search.listHisStorageConditionExpression)
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
