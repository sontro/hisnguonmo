using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationExam
{
    partial class HisVaccinationExamGet : EntityBase
    {
        public V_HIS_VACCINATION_EXAM GetViewByCode(string code, HisVaccinationExamSO search)
        {
            V_HIS_VACCINATION_EXAM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_VACCINATION_EXAM.AsQueryable().Where(p => p.VACCINATION_EXAM_CODE == code);
                        if (search.listVHisVaccinationExamExpression != null && search.listVHisVaccinationExamExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisVaccinationExamExpression)
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
