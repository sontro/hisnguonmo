using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientObservation
{
    partial class HisPatientObservationGet : BusinessBase
    {
        internal HIS_PATIENT_OBSERVATION GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisPatientObservationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PATIENT_OBSERVATION GetByCode(string code, HisPatientObservationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPatientObservationDAO.GetByCode(code, filter.Query());
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
