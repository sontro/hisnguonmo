using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDataStore
{
    public partial class HisDataStoreManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_DATA_STORE_1>> GetView1(HisDataStoreView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DATA_STORE_1>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DATA_STORE_1> resultData = null;
                if (valid)
                {
                    resultData = new HisDataStoreGet(param).GetView1(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }
    }
}
