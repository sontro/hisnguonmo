using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBillGoods
{
    public partial class HisBillGoodsManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BILL_GOODS>> GetView(HisBillGoodsViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BILL_GOODS>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BILL_GOODS> resultData = null;
                if (valid)
                {
                    resultData = new HisBillGoodsGet(param).GetView(filter);
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
