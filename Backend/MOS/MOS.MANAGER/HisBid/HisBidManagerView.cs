using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBid
{
    public partial class HisBidManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BID>> GetView(HisBidViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BID>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BID> resultData = null;
                if (valid)
                {
                    resultData = new HisBidGet(param).GetView(filter);
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

        [Logger]
        public ApiResultObject<List<V_HIS_BID>> GetViewBySupplier(long supplierId)
        {
            ApiResultObject<List<V_HIS_BID>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<V_HIS_BID> resultData = null;
                if (valid)
                {
                    resultData = new HisBidGet(param).GetViewBySupplier(supplierId);
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

        [Logger]
        public ApiResultObject<List<V_HIS_BID_1>> GetView1(HisBidView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BID_1>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BID_1> resultData = null;
                if (valid)
                {
                    resultData = new HisBidGet(param).GetView1(filter);
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
