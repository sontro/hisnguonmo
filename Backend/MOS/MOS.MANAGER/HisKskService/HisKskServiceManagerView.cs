using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskService
{
    public partial class HisKskServiceManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_KSK_SERVICE>> GetView(HisKskServiceViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_KSK_SERVICE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_KSK_SERVICE> resultData = null;
                if (valid)
                {
                    resultData = new HisKskServiceGet(param).GetView(filter);
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
