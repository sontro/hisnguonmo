using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentUnlimit
{
    public partial class HisTreatmentUnlimitManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_TREATMENT_UNLIMIT>> GetView(HisTreatmentUnlimitViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TREATMENT_UNLIMIT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TREATMENT_UNLIMIT> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentUnlimitGet(param).GetView(filter);
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
