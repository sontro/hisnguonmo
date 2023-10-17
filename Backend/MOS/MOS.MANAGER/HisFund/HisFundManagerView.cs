using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFund
{
    public partial class HisFundManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_FUND>> GetView(HisFundViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_FUND>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_FUND> resultData = null;
                if (valid)
                {
                    resultData = new HisFundGet(param).GetView(filter);
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
