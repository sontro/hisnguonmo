using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExamSereDire
{
    partial class HisExamSereDireGet : EntityBase
    {
        public V_HIS_EXAM_SERE_DIRE GetViewByCode(string code, HisExamSereDireSO search)
        {
            V_HIS_EXAM_SERE_DIRE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_EXAM_SERE_DIRE.AsQueryable().Where(p => p.EXAM_SERE_DIRE_CODE == code);
                        if (search.listVHisExamSereDireExpression != null && search.listVHisExamSereDireExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisExamSereDireExpression)
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
