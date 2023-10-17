using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientObservation
{
    public partial class HisPatientObservationManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PATIENT_OBSERVATION>> GetView(HisPatientObservationViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PATIENT_OBSERVATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PATIENT_OBSERVATION> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientObservationGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
