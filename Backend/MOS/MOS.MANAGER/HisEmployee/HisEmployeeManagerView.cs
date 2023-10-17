using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmployee
{
    public partial class HisEmployeeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EMPLOYEE>> GetView(HisEmployeeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EMPLOYEE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EMPLOYEE> resultData = null;
                if (valid)
                {
                    resultData = new HisEmployeeGet(param).GetView(filter);
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
