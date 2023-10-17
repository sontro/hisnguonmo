using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWorkingShift
{
    public partial class HisWorkingShiftManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_WORKING_SHIFT>> GetView(HisWorkingShiftViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_WORKING_SHIFT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_WORKING_SHIFT> resultData = null;
                if (valid)
                {
                    resultData = new HisWorkingShiftGet(param).GetView(filter);
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
