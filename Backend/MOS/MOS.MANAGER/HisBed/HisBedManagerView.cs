using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBed
{
    public partial class HisBedManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BED>> GetView(HisBedViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BED>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BED> resultData = null;
                if (valid)
                {
                    resultData = new HisBedGet(param).GetView(filter);
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
