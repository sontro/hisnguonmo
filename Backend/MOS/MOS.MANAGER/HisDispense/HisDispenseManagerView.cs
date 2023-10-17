using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDispense
{
    public partial class HisDispenseManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_DISPENSE>> GetView(HisDispenseViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DISPENSE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DISPENSE> resultData = null;
                if (valid)
                {
                    resultData = new HisDispenseGet(param).GetView(filter);
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
