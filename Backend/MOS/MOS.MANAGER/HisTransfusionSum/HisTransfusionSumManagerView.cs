using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusionSum
{
    public partial class HisTransfusionSumManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_TRANSFUSION_SUM>> GetView(HisTransfusionSumViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TRANSFUSION_SUM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TRANSFUSION_SUM> resultData = null;
                if (valid)
                {
                    resultData = new HisTransfusionSumGet(param).GetView(filter);
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
