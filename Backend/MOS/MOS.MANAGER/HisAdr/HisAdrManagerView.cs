using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAdr
{
    public partial class HisAdrManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ADR>> GetView(HisAdrViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ADR>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ADR> resultData = null;
                if (valid)
                {
                    resultData = new HisAdrGet(param).GetView(filter);
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
