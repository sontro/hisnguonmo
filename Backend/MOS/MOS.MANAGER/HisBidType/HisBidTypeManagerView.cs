using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidType
{
    public partial class HisBidTypeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BID_TYPE>> GetView(HisBidTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BID_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BID_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisBidTypeGet(param).GetView(filter);
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
