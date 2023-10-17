using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserGroupTempDt
{
    public partial class HisUserGroupTempDtManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_USER_GROUP_TEMP_DT>> GetView(HisUserGroupTempDtViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_USER_GROUP_TEMP_DT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_USER_GROUP_TEMP_DT> resultData = null;
                if (valid)
                {
                    resultData = new HisUserGroupTempDtGet(param).GetView(filter);
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
