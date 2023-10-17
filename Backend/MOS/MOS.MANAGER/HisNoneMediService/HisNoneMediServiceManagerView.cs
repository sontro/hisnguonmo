using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNoneMediService
{
    public partial class HisNoneMediServiceManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_NONE_MEDI_SERVICE>> GetView(HisNoneMediServiceViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_NONE_MEDI_SERVICE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_NONE_MEDI_SERVICE> resultData = null;
                if (valid)
                {
                    resultData = new HisNoneMediServiceGet(param).GetView(filter);
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
