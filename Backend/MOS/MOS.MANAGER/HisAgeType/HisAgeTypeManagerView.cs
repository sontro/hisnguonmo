using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAgeType
{
    public partial class HisAgeTypeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_AGE_TYPE>> GetView(HisAgeTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_AGE_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_AGE_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisAgeTypeGet(param).GetView(filter);
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
