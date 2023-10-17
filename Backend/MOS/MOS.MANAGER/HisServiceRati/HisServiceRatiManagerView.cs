using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRati
{
    public partial class HisServiceRatiManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SERVICE_RATI>> GetView(HisServiceRatiViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_RATI>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_RATI> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceRatiGet(param).GetView(filter);
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
