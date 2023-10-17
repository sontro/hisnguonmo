using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceNumOrder
{
    public partial class HisServiceNumOrderManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SERVICE_NUM_ORDER>> GetView(HisServiceNumOrderViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_NUM_ORDER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_NUM_ORDER> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceNumOrderGet(param).GetView(filter);
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
