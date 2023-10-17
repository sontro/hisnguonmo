using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPetroleum
{
    public partial class HisPetroleumManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PETROLEUM>> GetView(HisPetroleumViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PETROLEUM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PETROLEUM> resultData = null;
                if (valid)
                {
                    resultData = new HisPetroleumGet(param).GetView(filter);
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
