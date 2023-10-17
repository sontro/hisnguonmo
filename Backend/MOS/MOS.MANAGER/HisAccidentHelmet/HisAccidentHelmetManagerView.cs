using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentHelmet
{
    public partial class HisAccidentHelmetManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ACCIDENT_HELMET>> GetView(HisAccidentHelmetViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ACCIDENT_HELMET>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ACCIDENT_HELMET> resultData = null;
                if (valid)
                {
                    resultData = new HisAccidentHelmetGet(param).GetView(filter);
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
