using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCoTreatment
{
    public partial class HisCoTreatmentManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_CO_TREATMENT>> GetView(HisCoTreatmentViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CO_TREATMENT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CO_TREATMENT> resultData = null;
                if (valid)
                {
                    resultData = new HisCoTreatmentGet(param).GetView(filter);
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
