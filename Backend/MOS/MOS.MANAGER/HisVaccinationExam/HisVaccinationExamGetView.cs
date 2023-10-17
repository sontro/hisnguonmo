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
        internal List<V_HIS_VACCINATION_EXAM> GetView(HisVaccinationExamViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationExamDAO.GetView(filter.Query(), param);
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
