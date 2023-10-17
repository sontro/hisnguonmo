using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticRequest
{
    public partial class HisAntibioticRequestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ANTIBIOTIC_REQUEST>> GetView(HisAntibioticRequestViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ANTIBIOTIC_REQUEST>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ANTIBIOTIC_REQUEST> resultData = null;
                if (valid)
                {
                    resultData = new HisAntibioticRequestGet(param).GetView(filter);
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
