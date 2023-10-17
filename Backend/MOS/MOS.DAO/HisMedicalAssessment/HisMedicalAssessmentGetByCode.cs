using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicalAssessment
{
    partial class HisMedicalAssessmentGet : EntityBase
    {
        public HIS_MEDICAL_ASSESSMENT GetByCode(string code, HisMedicalAssessmentSO search)
        {
            HIS_MEDICAL_ASSESSMENT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MEDICAL_ASSESSMENT.AsQueryable().Where(p => p.MEDICAL_ASSESSMENT_CODE == code);
                        if (search.listHisMedicalAssessmentExpression != null && search.listHisMedicalAssessmentExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMedicalAssessmentExpression)
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
