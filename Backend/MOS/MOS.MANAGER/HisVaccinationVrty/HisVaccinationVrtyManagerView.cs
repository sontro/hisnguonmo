using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationVrty
{
    public partial class HisVaccinationVrtyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_VACCINATION_VRTY>> GetView(HisVaccinationVrtyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_VACCINATION_VRTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_VACCINATION_VRTY> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccinationVrtyGet(param).GetView(filter);
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
