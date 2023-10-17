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
        internal List<V_HIS_PATIENT_OBSERVATION> GetView(HisPatientObservationViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPatientObservationDAO.GetView(filter.Query(), param);
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
