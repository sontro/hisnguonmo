using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisQcNormation
{
    partial class HisQcNormationGet : EntityBase
    {
        public V_HIS_QC_NORMATION GetViewByCode(string code, HisQcNormationSO search)
        {
            V_HIS_QC_NORMATION result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_QC_NORMATION.AsQueryable().Where(p => p.QC_NORMATION_CODE == code);
                        if (search.listVHisQcNormationExpression != null && search.listVHisQcNormationExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisQcNormationExpression)
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
