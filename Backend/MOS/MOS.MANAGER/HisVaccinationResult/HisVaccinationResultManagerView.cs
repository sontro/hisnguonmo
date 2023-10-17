using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationResult
{
    public partial class HisVaccinationResultManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_VACCINATION_RESULT>> GetView(HisVaccinationResultViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_VACCINATION_RESULT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_VACCINATION_RESULT> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccinationResultGet(param).GetView(filter);
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
