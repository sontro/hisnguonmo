using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkip
{
    public partial class HisEkipManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EKIP>> GetView(HisEkipViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EKIP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EKIP> resultData = null;
                if (valid)
                {
                    resultData = new HisEkipGet(param).GetView(filter);
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
