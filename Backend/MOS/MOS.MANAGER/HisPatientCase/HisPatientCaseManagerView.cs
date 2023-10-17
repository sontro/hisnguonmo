using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientCase
{
    public partial class HisPatientCaseManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PATIENT_CASE>> GetView(HisPatientCaseViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PATIENT_CASE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PATIENT_CASE> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientCaseGet(param).GetView(filter);
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
