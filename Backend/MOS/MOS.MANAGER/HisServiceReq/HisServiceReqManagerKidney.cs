using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Kidney.KidneySchedule;
using MOS.MANAGER.HisServiceReq.Paan;
using MOS.MANAGER.HisServiceReq.Reha;
using MOS.MANAGER.HisServiceReq.Surg;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<HisServiceReqListResultSDO> KidneySchedule(HisServiceReqKidneyScheduleSDO data)
        {
            ApiResultObject<HisServiceReqListResultSDO> result = new ApiResultObject<HisServiceReqListResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisServiceReqListResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqKidneyScheduler(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
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
