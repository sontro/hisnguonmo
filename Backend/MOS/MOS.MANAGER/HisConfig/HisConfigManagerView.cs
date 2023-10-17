using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisConfig
{
    public partial class HisConfigManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_CONFIG>> GetView(HisConfigViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CONFIG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CONFIG> resultData = null;
                if (valid)
                {
                    resultData = new HisConfigGet(param).GetView(filter);
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
