using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodGiver
{
    public partial class HisBloodGiverManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BLOOD_GIVER>> GetView(HisBloodGiverViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BLOOD_GIVER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BLOOD_GIVER> resultData = null;
                if (valid)
                {
                    resultData = new HisBloodGiverGet(param).GetView(filter);
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
