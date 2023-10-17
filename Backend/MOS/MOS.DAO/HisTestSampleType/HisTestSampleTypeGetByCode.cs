using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTestSampleType
{
    partial class HisTestSampleTypeGet : EntityBase
    {
        public HIS_TEST_SAMPLE_TYPE GetByCode(string code, HisTestSampleTypeSO search)
        {
            HIS_TEST_SAMPLE_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_TEST_SAMPLE_TYPE.AsQueryable().Where(p => p.TEST_SAMPLE_TYPE_CODE == code);
                        if (search.listHisTestSampleTypeExpression != null && search.listHisTestSampleTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisTestSampleTypeExpression)
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
