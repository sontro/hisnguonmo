using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationExam
{
    partial class HisVaccinationExamGet : BusinessBase
    {
        internal V_HIS_VACCINATION_EXAM GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisVaccinationExamViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_VACCINATION_EXAM GetViewByCode(string code, HisVaccinationExamViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationExamDAO.GetViewByCode(code, filter.Query());
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
