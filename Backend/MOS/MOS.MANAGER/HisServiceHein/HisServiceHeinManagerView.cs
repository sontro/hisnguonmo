using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceHein
{
    public partial class HisServiceHeinManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SERVICE_HEIN>> GetView(HisServiceHeinViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_HEIN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_HEIN> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceHeinGet(param).GetView(filter);
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
