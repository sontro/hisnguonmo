using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransaction
{
    public partial class HisTransactionManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_TRANSACTION_5>> GetView5(HisTransactionView5FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TRANSACTION_5>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TRANSACTION_5> resultData = new List<V_HIS_TRANSACTION_5>();
                if (valid)
                {
                    resultData = new HisTransactionGet(param).GetView5(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }
    }
}
