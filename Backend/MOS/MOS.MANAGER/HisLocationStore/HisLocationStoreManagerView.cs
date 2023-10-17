using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisLocationStore
{
    public partial class HisLocationStoreManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_LOCATION_STORE>> GetView(HisLocationStoreViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_LOCATION_STORE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_LOCATION_STORE> resultData = null;
                if (valid)
                {
                    resultData = new HisLocationStoreGet(param).GetView(filter);
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
