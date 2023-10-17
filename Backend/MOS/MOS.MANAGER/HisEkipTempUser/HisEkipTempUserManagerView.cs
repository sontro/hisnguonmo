using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipTempUser
{
    public partial class HisEkipTempUserManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EKIP_TEMP_USER>> GetView(HisEkipTempUserViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EKIP_TEMP_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EKIP_TEMP_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisEkipTempUserGet(param).GetView(filter);
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
