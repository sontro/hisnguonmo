using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestPropose
{
    public partial class HisImpMestProposeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_IMP_MEST_PROPOSE>> GetView(HisImpMestProposeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_IMP_MEST_PROPOSE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_IMP_MEST_PROPOSE> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestProposeGet(param).GetView(filter);
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
