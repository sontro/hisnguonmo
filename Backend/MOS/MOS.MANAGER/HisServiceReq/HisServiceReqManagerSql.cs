using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<D_HIS_SERVICE_REQ_2>> GetDHisServiceReq2(DHisServiceReq2Filter filter)
        {
            ApiResultObject<List<D_HIS_SERVICE_REQ_2>> result = null;

            try
            {
                List<D_HIS_SERVICE_REQ_2> resultData = null;
                resultData = new HisServiceReqGet(param).GetDHisServiceReq2(filter);
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

        [Logger]
        public ApiResultObject<long> GetCount(HisServiceReqCountFilter filter)
        {
            ApiResultObject<long> result = null;

            try
            {
                long resultData = new HisServiceReqGet(param).GetCount(filter);
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

        [Logger]
        public ApiResultObject<List<HisServiceReqMaxNumOrderSDO>> GetMaxNumOrder(HisServiceReqMaxNumOrderFilter filter)
        {
            ApiResultObject<List<HisServiceReqMaxNumOrderSDO>> result = null;

            try
            {
                List<HisServiceReqMaxNumOrderSDO> resultData = new HisServiceReqGet(param).GetMaxNumOrder(filter);
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
