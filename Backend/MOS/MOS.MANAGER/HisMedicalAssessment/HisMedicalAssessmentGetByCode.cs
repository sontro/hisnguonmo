using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicalAssessment
{
    partial class HisMedicalAssessmentGet : BusinessBase
    {
        internal HIS_MEDICAL_ASSESSMENT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMedicalAssessmentFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICAL_ASSESSMENT GetByCode(string code, HisMedicalAssessmentFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicalAssessmentDAO.GetByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
