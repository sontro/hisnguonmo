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
        internal List<V_HIS_MEDICAL_ASSESSMENT> GetView(HisMedicalAssessmentViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicalAssessmentDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDICAL_ASSESSMENT GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisMedicalAssessmentViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDICAL_ASSESSMENT GetViewById(long id, HisMedicalAssessmentViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicalAssessmentDAO.GetViewById(id, filter.Query());
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
