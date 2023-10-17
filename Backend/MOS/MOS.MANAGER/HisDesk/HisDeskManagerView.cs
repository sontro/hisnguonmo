using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDesk
{
    public partial class HisDeskManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_DESK>> GetView(HisDeskViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DESK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DESK> resultData = null;
                if (valid)
                {
                    resultData = new HisDeskGet(param).GetView(filter);
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
