using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisProcessingMethod
{
    partial class HisProcessingMethodGet : EntityBase
    {
        public V_HIS_PROCESSING_METHOD GetViewByCode(string code, HisProcessingMethodSO search)
        {
            V_HIS_PROCESSING_METHOD result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_PROCESSING_METHOD.AsQueryable().Where(p => p.PROCESSING_METHOD_CODE == code);
                        if (search.listVHisProcessingMethodExpression != null && search.listVHisProcessingMethodExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisProcessingMethodExpression)
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
