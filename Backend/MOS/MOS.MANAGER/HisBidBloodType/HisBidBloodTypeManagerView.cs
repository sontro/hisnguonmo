using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidBloodType
{
    public partial class HisBidBloodTypeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BID_BLOOD_TYPE>> GetView(HisBidBloodTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BID_BLOOD_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BID_BLOOD_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisBidBloodTypeGet(param).GetView(filter);
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
