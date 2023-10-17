using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisNoneMediService
{
    partial class HisNoneMediServiceGet : EntityBase
    {
        public V_HIS_NONE_MEDI_SERVICE GetViewByCode(string code, HisNoneMediServiceSO search)
        {
            V_HIS_NONE_MEDI_SERVICE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_NONE_MEDI_SERVICE.AsQueryable().Where(p => p.NONE_MEDI_SERVICE_CODE == code);
                        if (search.listVHisNoneMediServiceExpression != null && search.listVHisNoneMediServiceExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisNoneMediServiceExpression)
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
