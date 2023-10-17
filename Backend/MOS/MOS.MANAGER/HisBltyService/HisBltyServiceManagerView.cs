using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBltyService
{
    public partial class HisBltyServiceManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BLTY_SERVICE>> GetView(HisBltyServiceViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BLTY_SERVICE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BLTY_SERVICE> resultData = null;
                if (valid)
                {
                    resultData = new HisBltyServiceGet(param).GetView(filter);
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
