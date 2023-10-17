using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipTemp
{
    public partial class HisEkipTempManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EKIP_TEMP>> GetView(HisEkipTempViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EKIP_TEMP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EKIP_TEMP> resultData = null;
                if (valid)
                {
                    resultData = new HisEkipTempGet(param).GetView(filter);
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
