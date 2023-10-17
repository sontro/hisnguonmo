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
        internal V_HIS_PATIENT_OBSERVATION GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisPatientObservationViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_PATIENT_OBSERVATION GetViewByCode(string code, HisPatientObservationViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPatientObservationDAO.GetViewByCode(code, filter.Query());
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
