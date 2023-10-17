using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeSub
{
    public partial class HisPatientTypeSubManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PATIENT_TYPE_SUB>> GetView(HisPatientTypeSubViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PATIENT_TYPE_SUB>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PATIENT_TYPE_SUB> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientTypeSubGet(param).GetView(filter);
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
