using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInfusionSum
{
    public partial class HisInfusionSumManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_INFUSION_SUM>> GetView(HisInfusionSumViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_INFUSION_SUM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_INFUSION_SUM> resultData = null;
                if (valid)
                {
                    resultData = new HisInfusionSumGet(param).GetView(filter);
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
