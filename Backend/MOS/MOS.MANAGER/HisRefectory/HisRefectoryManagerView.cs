using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRefectory
{
    public partial class HisRefectoryManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_REFECTORY>> GetView(HisRefectoryViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_REFECTORY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_REFECTORY> resultData = null;
                if (valid)
                {
                    resultData = new HisRefectoryGet(param).GetView(filter);
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
