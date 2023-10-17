using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodAbo
{
    public partial class HisBloodAboManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BLOOD_ABO>> GetView(HisBloodAboViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BLOOD_ABO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BLOOD_ABO> resultData = null;
                if (valid)
                {
                    resultData = new HisBloodAboGet(param).GetView(filter);
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
