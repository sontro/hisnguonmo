using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccination
{
    public partial class HisVaccinationManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_VACCINATION>> GetView(HisVaccinationViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_VACCINATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_VACCINATION> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccinationGet(param).GetView(filter);
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
