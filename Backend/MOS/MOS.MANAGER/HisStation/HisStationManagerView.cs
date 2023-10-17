using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStation
{
    public partial class HisStationManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_STATION>> GetView(HisStationViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_STATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_STATION> resultData = null;
                if (valid)
                {
                    resultData = new HisStationGet(param).GetView(filter);
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
