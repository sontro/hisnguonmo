using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHospitalizeReason
{
    public partial class HisHospitalizeReasonManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_HOSPITALIZE_REASON>> GetView(HisHospitalizeReasonViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_HOSPITALIZE_REASON>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_HOSPITALIZE_REASON> resultData = null;
                if (valid)
                {
                    resultData = new HisHospitalizeReasonGet(param).GetView(filter);
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
