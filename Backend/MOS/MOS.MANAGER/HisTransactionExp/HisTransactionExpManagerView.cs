using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransactionExp
{
    public partial class HisTransactionExpManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_TRANSACTION_EXP>> GetView(HisTransactionExpViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TRANSACTION_EXP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TRANSACTION_EXP> resultData = null;
                if (valid)
                {
                    resultData = new HisTransactionExpGet(param).GetView(filter);
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
