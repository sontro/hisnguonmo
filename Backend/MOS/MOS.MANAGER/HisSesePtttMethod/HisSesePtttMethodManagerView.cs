using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSesePtttMethod
{
    public partial class HisSesePtttMethodManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SESE_PTTT_METHOD>> GetView(HisSesePtttMethodViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SESE_PTTT_METHOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SESE_PTTT_METHOD> resultData = null;
                if (valid)
                {
                    resultData = new HisSesePtttMethodGet(param).GetView(filter);
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
