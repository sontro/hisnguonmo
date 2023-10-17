using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisConfigGroup
{
    public partial class HisConfigGroupManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_CONFIG_GROUP>> GetView(HisConfigGroupViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CONFIG_GROUP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CONFIG_GROUP> resultData = null;
                if (valid)
                {
                    resultData = new HisConfigGroupGet(param).GetView(filter);
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
