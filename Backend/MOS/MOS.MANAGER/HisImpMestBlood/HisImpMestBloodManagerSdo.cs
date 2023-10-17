using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestBlood
{
    public partial class HisImpMestBloodManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<HisImpMestBloodWithInStockInfoSDO>> GetViewWithInStockInfo(long impMestId)
        {
            ApiResultObject<List<HisImpMestBloodWithInStockInfoSDO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisImpMestBloodWithInStockInfoSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestBloodGet(param).GetViewWithInStockInfo(impMestId);
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
