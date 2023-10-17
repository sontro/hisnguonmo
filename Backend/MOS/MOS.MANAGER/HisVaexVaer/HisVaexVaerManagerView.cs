using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaexVaer
{
    public partial class HisVaexVaerManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_VAEX_VAER>> GetView(HisVaexVaerViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_VAEX_VAER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_VAEX_VAER> resultData = null;
                if (valid)
                {
                    resultData = new HisVaexVaerGet(param).GetView(filter);
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
