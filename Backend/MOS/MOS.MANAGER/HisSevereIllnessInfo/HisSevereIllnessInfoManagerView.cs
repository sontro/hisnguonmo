using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSevereIllnessInfo
{
    public partial class HisSevereIllnessInfoManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SEVERE_ILLNESS_INFO>> GetView(HisSevereIllnessInfoViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SEVERE_ILLNESS_INFO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SEVERE_ILLNESS_INFO> resultData = null;
                if (valid)
                {
                    resultData = new HisSevereIllnessInfoGet(param).GetView(filter);
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
