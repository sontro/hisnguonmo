using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHivTreatment
{
    public partial class HisHivTreatmentManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_HIV_TREATMENT>> GetView(HisHivTreatmentViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_HIV_TREATMENT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_HIV_TREATMENT> resultData = null;
                if (valid)
                {
                    resultData = new HisHivTreatmentGet(param).GetView(filter);
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
