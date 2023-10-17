using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpBltyService
{
    public partial class HisExpBltyServiceManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXP_BLTY_SERVICE>> GetView(HisExpBltyServiceViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXP_BLTY_SERVICE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXP_BLTY_SERVICE> resultData = null;
                if (valid)
                {
                    resultData = new HisExpBltyServiceGet(param).GetView(filter);
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
