using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHeinApproval
{
    partial class HisHeinApprovalGet : EntityBase
    {
        public HIS_HEIN_APPROVAL GetByCode(string code, HisHeinApprovalSO search)
        {
            HIS_HEIN_APPROVAL result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_HEIN_APPROVAL.AsQueryable().Where(p => p.HEIN_APPROVAL_CODE == code);
                        if (search.listHisHeinApprovalExpression != null && search.listHisHeinApprovalExpression.Count > 0)
                        {
                            foreach (var item in search.listHisHeinApprovalExpression)
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
