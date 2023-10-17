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
        internal V_HIS_MEDICAL_ASSESSMENT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMedicalAssessmentViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDICAL_ASSESSMENT GetViewByCode(string code, HisMedicalAssessmentViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicalAssessmentDAO.GetViewByCode(code, filter.Query());
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
