using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedBsty
{
    public partial class HisBedBstyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BED_BSTY>> GetView(HisBedBstyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BED_BSTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BED_BSTY> resultData = null;
                if (valid)
                {
                    resultData = new HisBedBstyGet(param).GetView(filter);
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
