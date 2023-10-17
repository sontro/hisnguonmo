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
        internal HIS_VACCINATION_EXAM GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisVaccinationExamFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_EXAM GetByCode(string code, HisVaccinationExamFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationExamDAO.GetByCode(code, filter.Query());
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
