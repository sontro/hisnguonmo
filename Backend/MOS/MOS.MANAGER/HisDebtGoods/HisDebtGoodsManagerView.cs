using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebtGoods
{
    public partial class HisDebtGoodsManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_DEBT_GOODS>> GetView(HisDebtGoodsViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DEBT_GOODS>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DEBT_GOODS> resultData = null;
                if (valid)
                {
                    resultData = new HisDebtGoodsGet(param).GetView(filter);
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
