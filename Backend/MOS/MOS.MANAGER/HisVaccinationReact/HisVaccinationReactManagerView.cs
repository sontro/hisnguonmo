using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationReact
{
    public partial class HisVaccinationReactManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_VACCINATION_REACT>> GetView(HisVaccinationReactViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_VACCINATION_REACT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_VACCINATION_REACT> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccinationReactGet(param).GetView(filter);
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
