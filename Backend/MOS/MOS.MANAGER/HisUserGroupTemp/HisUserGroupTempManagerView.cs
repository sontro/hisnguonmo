using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserGroupTemp
{
    public partial class HisUserGroupTempManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_USER_GROUP_TEMP>> GetView(HisUserGroupTempViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_USER_GROUP_TEMP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_USER_GROUP_TEMP> resultData = null;
                if (valid)
                {
                    resultData = new HisUserGroupTempGet(param).GetView(filter);
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
