using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    public partial class HisCarerCardBorrowManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_CARER_CARD_BORROW>> GetView(HisCarerCardBorrowViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CARER_CARD_BORROW>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CARER_CARD_BORROW> resultData = null;
                if (valid)
                {
                    resultData = new HisCarerCardBorrowGet(param).GetView(filter);
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
