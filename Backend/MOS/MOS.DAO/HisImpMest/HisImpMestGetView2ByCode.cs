using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMest
{
    partial class HisImpMestGet : EntityBase
    {
        public V_HIS_IMP_MEST_2 GetView2ByCode(string code, HisImpMestSO search)
        {
            V_HIS_IMP_MEST_2 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_IMP_MEST_2.AsQueryable().Where(p => p.IMP_MEST_CODE == code);
                        if (search.listVHisImpMest2Expression != null && search.listVHisImpMest2Expression.Count > 0)
                        {
                            foreach (var item in search.listVHisImpMest2Expression)
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