using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCarerCard
{
    public partial class HisCarerCardManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_CARER_CARD>> GetView(HisCarerCardViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CARER_CARD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CARER_CARD> resultData = null;
                if (valid)
                {
                    resultData = new HisCarerCardGet(param).GetView(filter);
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
