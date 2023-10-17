using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInteractiveGrade
{
    partial class HisInteractiveGradeGet : EntityBase
    {
        public HIS_INTERACTIVE_GRADE GetByCode(string code, HisInteractiveGradeSO search)
        {
            HIS_INTERACTIVE_GRADE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_INTERACTIVE_GRADE.AsQueryable().Where(p => p.INTERACTIVE_GRADE_CODE == code);
                        if (search.listHisInteractiveGradeExpression != null && search.listHisInteractiveGradeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisInteractiveGradeExpression)
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
