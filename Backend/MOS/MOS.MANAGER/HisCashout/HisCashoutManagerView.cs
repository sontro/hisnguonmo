using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashout
{
    public partial class HisCashoutManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_CASHOUT>> GetView(HisCashoutViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CASHOUT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CASHOUT> resultData = null;
                if (valid)
                {
                    resultData = new HisCashoutGet(param).GetView(filter);
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
