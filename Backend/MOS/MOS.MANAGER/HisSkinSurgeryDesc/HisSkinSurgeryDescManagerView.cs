using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSkinSurgeryDesc
{
    public partial class HisSkinSurgeryDescManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SKIN_SURGERY_DESC>> GetView(HisSkinSurgeryDescViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SKIN_SURGERY_DESC>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SKIN_SURGERY_DESC> resultData = null;
                if (valid)
                {
                    resultData = new HisSkinSurgeryDescGet(param).GetView(filter);
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
