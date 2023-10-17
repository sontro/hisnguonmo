using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServBill
{
    public partial class HisSereServBillManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SERE_SERV_BILL>> GetView(HisSereServBillViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERE_SERV_BILL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERE_SERV_BILL> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServBillGet(param).GetView(filter);
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
