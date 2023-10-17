using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Bed;
using MOS.MANAGER.HisServiceReq.Exam;
using MOS.MANAGER.HisServiceReq.Optometrist;
using MOS.MANAGER.HisServiceReq.Paan;
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
        public ApiResultObject<HIS_SERVICE_REQ> Optometrist(HisServiceReqOptometristSDO data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqOptometristProcess(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
