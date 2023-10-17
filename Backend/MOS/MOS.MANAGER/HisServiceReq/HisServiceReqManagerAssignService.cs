using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.LogManager;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.MANAGER.HisServiceReq.Bed;
using MOS.MANAGER.HisServiceReq.Paan;
using MOS.MANAGER.HisServiceReq.Surg;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<HisServiceReqListResultSDO> AssignService(AssignServiceSDO data)
        {
            ApiResultObject<HisServiceReqListResultSDO> result = new ApiResultObject<HisServiceReqListResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                HisServiceReqListResultSDO resultData = null;
                bool rs = false;
                if (valid)
                {
                    rs = new HisServiceReqAssignServiceCreate(param).Create(data, true, ref resultData);
                }
                result = this.PackResult(resultData, rs);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisServiceReqListResultSDO> AssignServiceByInstructionTimes(AssignServiceSDO data)
        {
            ApiResultObject<HisServiceReqListResultSDO> result = new ApiResultObject<HisServiceReqListResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                HisServiceReqListResultSDO resultData = null;
                bool rs = false;
                if (valid)
                {
                    rs = new HisServiceReqAssignServiceCreate(param).Create(data, true, ref resultData);
                }
                result = this.PackResult(resultData, rs);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisServiceReqListResultSDO> AssignTestForBlood(AssignTestForBloodSDO data)
        {
            ApiResultObject<HisServiceReqListResultSDO> result = new ApiResultObject<HisServiceReqListResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                HisServiceReqListResultSDO resultData = null;
                bool rs = false;

                if (valid)
                {
                    rs = new HisServiceReqAssignTestForBlood(param).Create(data, ref resultData);
                }
                result = this.PackResult(resultData, rs);
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
