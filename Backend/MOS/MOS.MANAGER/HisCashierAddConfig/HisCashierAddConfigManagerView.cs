using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashierAddConfig
{
    public partial class HisCashierAddConfigManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_CASHIER_ADD_CONFIG>> GetView(HisCashierAddConfigViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CASHIER_ADD_CONFIG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CASHIER_ADD_CONFIG> resultData = null;
                if (valid)
                {
                    resultData = new HisCashierAddConfigGet(param).GetView(filter);
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
