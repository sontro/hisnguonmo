using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAllergyCard
{
    public partial class HisAllergyCardManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ALLERGY_CARD>> GetView(HisAllergyCardViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ALLERGY_CARD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ALLERGY_CARD> resultData = null;
                if (valid)
                {
                    resultData = new HisAllergyCardGet(param).GetView(filter);
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
