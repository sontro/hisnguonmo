using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq.Bed.CreateByBedLog;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<HisServiceReqListResultSDO> CreateByBedLog(HisBedServiceReqSDO data)
        {
            ApiResultObject<HisServiceReqListResultSDO> result = new ApiResultObject<HisServiceReqListResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisServiceReqListResultSDO resultData = null;
                bool rs = false;
                if (valid)
                {
                    rs = new HisServiceReqBedCreate(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, rs);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }
    }
}
