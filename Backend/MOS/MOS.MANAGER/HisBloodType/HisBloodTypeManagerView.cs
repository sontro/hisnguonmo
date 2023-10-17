using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodType
{
    public partial class HisBloodTypeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BLOOD_TYPE>> GetView(HisBloodTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BLOOD_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BLOOD_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisBloodTypeGet(param).GetView(filter);
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
