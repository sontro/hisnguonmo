using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPaanLiquid
{
    public partial class HisPaanLiquidManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PAAN_LIQUID>> GetView(HisPaanLiquidViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PAAN_LIQUID>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PAAN_LIQUID> resultData = null;
                if (valid)
                {
                    resultData = new HisPaanLiquidGet(param).GetView(filter);
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
