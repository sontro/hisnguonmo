using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationVrpl
{
    public partial class HisVaccinationVrplManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_VACCINATION_VRPL>> GetView(HisVaccinationVrplViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_VACCINATION_VRPL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_VACCINATION_VRPL> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccinationVrplGet(param).GetView(filter);
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
