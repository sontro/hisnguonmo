using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestPropose
{
    partial class HisImpMestProposeGet : EntityBase
    {
        public HIS_IMP_MEST_PROPOSE GetByCode(string code, HisImpMestProposeSO search)
        {
            HIS_IMP_MEST_PROPOSE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_IMP_MEST_PROPOSE.AsQueryable().Where(p => p.IMP_MEST_PROPOSE_CODE == code);
                        if (search.listHisImpMestProposeExpression != null && search.listHisImpMestProposeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisImpMestProposeExpression)
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
