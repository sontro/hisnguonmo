using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationStt
{
    public partial class HisVaccinationSttManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_VACCINATION_STT>> GetView(HisVaccinationSttViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_VACCINATION_STT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_VACCINATION_STT> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccinationSttGet(param).GetView(filter);
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
