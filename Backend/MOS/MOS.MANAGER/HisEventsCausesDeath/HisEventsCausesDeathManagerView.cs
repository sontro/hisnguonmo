using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEventsCausesDeath
{
    public partial class HisEventsCausesDeathManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EVENTS_CAUSES_DEATH>> GetView(HisEventsCausesDeathViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EVENTS_CAUSES_DEATH>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EVENTS_CAUSES_DEATH> resultData = null;
                if (valid)
                {
                    resultData = new HisEventsCausesDeathGet(param).GetView(filter);
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
