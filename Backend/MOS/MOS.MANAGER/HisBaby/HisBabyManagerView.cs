using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBaby
{
    public partial class HisBabyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BABY>> GetView(HisBabyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BABY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BABY> resultData = null;
                if (valid)
                {
                    resultData = new HisBabyGet(param).GetView(filter);
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
