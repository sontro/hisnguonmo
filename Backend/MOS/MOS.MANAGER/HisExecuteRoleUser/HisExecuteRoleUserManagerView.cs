using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteRoleUser
{
    public partial class HisExecuteRoleUserManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXECUTE_ROLE_USER>> GetView(HisExecuteRoleUserViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXECUTE_ROLE_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXECUTE_ROLE_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisExecuteRoleUserGet(param).GetView(filter);
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
