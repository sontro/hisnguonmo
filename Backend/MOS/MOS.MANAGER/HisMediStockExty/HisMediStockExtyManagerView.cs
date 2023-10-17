using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockExty
{
    public partial class HisMediStockExtyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEDI_STOCK_EXTY>> GetView(HisMediStockExtyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_STOCK_EXTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_STOCK_EXTY> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockExtyGet(param).GetView(filter);
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
