using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodType
{
    public partial class HisBloodTypeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<HisBloodTypeInStockSDO>> GetInStockBloodType(HisBloodTypeStockViewFilter filter)
        {
            ApiResultObject<List<HisBloodTypeInStockSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisBloodTypeInStockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisBloodTypeGet(param).GetInStockBloodType(filter);
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
