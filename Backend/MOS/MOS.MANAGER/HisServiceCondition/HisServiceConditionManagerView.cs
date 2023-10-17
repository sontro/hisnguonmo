using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceCondition
{
    public partial class HisServiceConditionManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SERVICE_CONDITION>> GetView(HisServiceConditionViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_CONDITION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_CONDITION> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceConditionGet(param).GetView(filter);
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
