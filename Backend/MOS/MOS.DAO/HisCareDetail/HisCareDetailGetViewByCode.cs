using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCareDetail
{
    partial class HisCareDetailGet : EntityBase
    {
        public V_HIS_CARE_DETAIL GetViewByCode(string code, HisCareDetailSO search)
        {
            V_HIS_CARE_DETAIL result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_CARE_DETAIL.AsQueryable().Where(p => p.CARE_DETAIL_CODE == code);
                        if (search.listVHisCareDetailExpression != null && search.listVHisCareDetailExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisCareDetailExpression)
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
