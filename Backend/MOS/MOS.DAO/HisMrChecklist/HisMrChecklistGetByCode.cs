using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrChecklist
{
    partial class HisMrChecklistGet : EntityBase
    {
        public HIS_MR_CHECKLIST GetByCode(string code, HisMrChecklistSO search)
        {
            HIS_MR_CHECKLIST result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MR_CHECKLIST.AsQueryable().Where(p => p.MR_CHECKLIST_CODE == code);
                        if (search.listHisMrChecklistExpression != null && search.listHisMrChecklistExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMrChecklistExpression)
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
