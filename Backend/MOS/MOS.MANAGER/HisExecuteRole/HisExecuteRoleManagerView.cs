using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteRole
{
    public partial class HisExecuteRoleManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXECUTE_ROLE>> GetView(HisExecuteRoleViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXECUTE_ROLE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXECUTE_ROLE> resultData = null;
                if (valid)
                {
                    resultData = new HisExecuteRoleGet(param).GetView(filter);
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
