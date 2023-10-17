using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Paan;
using MOS.MANAGER.HisServiceReq.Surg;
using MOS.MANAGER.HisServiceReq.Surg.Calendar;
using MOS.MANAGER.HisServiceReq.Surg.SurgAssignAndCopy;
using MOS.MANAGER.HisServiceReq.Surg.Planning;
using MOS.MANAGER.HisServiceReq.Surg.Process;
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
        public ApiResultObject<HisSurgServiceReqUpdateSDO> SurgUpdate(HisSurgServiceReqUpdateSDO data)
        {
            ApiResultObject<HisSurgServiceReqUpdateSDO> result = new ApiResultObject<HisSurgServiceReqUpdateSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisSurgServiceReqUpdateSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqSurgProcess(param).Run(data, ref resultData);
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

        [Logger]
        public ApiResultObject<HisSurgServiceReqUpdateListSDO> SurgUpdateList(HisSurgServiceReqUpdateListSDO data)
        {
            ApiResultObject<HisSurgServiceReqUpdateListSDO> result = new ApiResultObject<HisSurgServiceReqUpdateListSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisSurgServiceReqUpdateListSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqSurgProcessList(param).Run(data, ref resultData);
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

        [Logger]
        public ApiResultObject<HIS_SERVICE_REQ> SurgPlan(HisServiceReqPlanSDO data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ resultData = null;
                if (valid)
                {
                    new HisServiceReqSurgPlan(param).Run(data, ref resultData);
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

        [Logger]
        public ApiResultObject<bool> SurgCalendarAdd(HisServiceReqCalendarSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceReqSurgCalendar(param).Add(data);
                }
                result = this.PackResult(resultData, resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<bool> SurgCalendarRemove(HisServiceReqCalendarSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceReqSurgCalendar(param).Remove(data);
                }
                result = this.PackResult(resultData, resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_SERVICE_REQ> SurgPlanApprove(HisServiceReqPlanApproveSDO data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceReqSurgPlanApprove(param).Approve(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_SERVICE_REQ> SurgPlanReject(HisServiceReqPlanApproveSDO data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceReqSurgPlanApprove(param).Reject(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_SERVICE_REQ> SurgPlanUnreject(HisServiceReqPlanApproveSDO data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceReqSurgPlanApprove(param).Unreject(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_SERVICE_REQ> SurgPlanUnapprove(HisServiceReqPlanApproveSDO data)
        {
            ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceReqSurgPlanApprove(param).Unapprove(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<bool> SurgAssignAndCopy(SurgAssignAndCopySDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new SurgAssignAndCopyProcessor(param).Run(data);
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
