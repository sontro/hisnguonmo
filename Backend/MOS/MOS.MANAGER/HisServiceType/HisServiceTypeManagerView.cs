using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceType
{
    public partial class HisServiceTypeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SERVICE_TYPE>> GetView(HisServiceTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceTypeGet(param).GetView(filter);
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
