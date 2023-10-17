using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticMicrobi
{
    public partial class HisAntibioticMicrobiManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ANTIBIOTIC_MICROBI>> GetView(HisAntibioticMicrobiViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ANTIBIOTIC_MICROBI>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ANTIBIOTIC_MICROBI> resultData = null;
                if (valid)
                {
                    resultData = new HisAntibioticMicrobiGet(param).GetView(filter);
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
