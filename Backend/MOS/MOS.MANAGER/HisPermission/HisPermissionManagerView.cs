using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPermission
{
    public partial class HisPermissionManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PERMISSION>> GetView(HisPermissionViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PERMISSION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PERMISSION> resultData = null;
                if (valid)
                {
                    resultData = new HisPermissionGet(param).GetView(filter);
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
