using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTestIndexRange
{
    partial class HisTestIndexRangeGet : EntityBase
    {
        public HIS_TEST_INDEX_RANGE GetByCode(string code, HisTestIndexRangeSO search)
        {
            HIS_TEST_INDEX_RANGE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_TEST_INDEX_RANGE.AsQueryable().Where(p => p.TEST_INDEX_RANGE_CODE == code);
                        if (search.listHisTestIndexRangeExpression != null && search.listHisTestIndexRangeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisTestIndexRangeExpression)
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
