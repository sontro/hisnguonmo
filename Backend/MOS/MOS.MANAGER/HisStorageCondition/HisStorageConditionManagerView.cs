using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStorageCondition
{
    public partial class HisStorageConditionManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_STORAGE_CONDITION>> GetView(HisStorageConditionViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_STORAGE_CONDITION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_STORAGE_CONDITION> resultData = null;
                if (valid)
                {
                    resultData = new HisStorageConditionGet(param).GetView(filter);
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
