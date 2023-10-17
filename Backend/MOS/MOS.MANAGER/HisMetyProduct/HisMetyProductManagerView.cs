using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyProduct
{
    public partial class HisMetyProductManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_METY_PRODUCT>> GetView(HisMetyProductViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_METY_PRODUCT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_METY_PRODUCT> resultData = null;
                if (valid)
                {
                    resultData = new HisMetyProductGet(param).GetView(filter);
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
