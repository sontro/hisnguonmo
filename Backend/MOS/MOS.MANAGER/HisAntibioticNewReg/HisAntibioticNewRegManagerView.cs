using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticNewReg
{
    public partial class HisAntibioticNewRegManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ANTIBIOTIC_NEW_REG>> GetView(HisAntibioticNewRegViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ANTIBIOTIC_NEW_REG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ANTIBIOTIC_NEW_REG> resultData = null;
                if (valid)
                {
                    resultData = new HisAntibioticNewRegGet(param).GetView(filter);
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
