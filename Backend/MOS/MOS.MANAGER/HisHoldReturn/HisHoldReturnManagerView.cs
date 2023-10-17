using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoldReturn
{
    public partial class HisHoldReturnManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_HOLD_RETURN>> GetView(HisHoldReturnViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_HOLD_RETURN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_HOLD_RETURN> resultData = null;
                if (valid)
                {
                    resultData = new HisHoldReturnGet(param).GetView(filter);
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
