using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationGroup
{
    public partial class HisRationGroupManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_RATION_GROUP>> GetView(HisRationGroupViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_RATION_GROUP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_RATION_GROUP> resultData = null;
                if (valid)
                {
                    resultData = new HisRationGroupGet(param).GetView(filter);
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
