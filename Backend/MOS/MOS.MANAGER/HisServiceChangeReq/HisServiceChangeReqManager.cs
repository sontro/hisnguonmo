using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisServiceChangeReq.Create;
using MOS.MANAGER.HisServiceChangeReq.Approve;
using MOS.MANAGER.HisServiceChangeReq.CashierApprove;

namespace MOS.MANAGER.HisServiceChangeReq
{
    public partial class HisServiceChangeReqManager : BusinessBase
    {
        public HisServiceChangeReqManager()
            : base()
        {

        }
        
        public HisServiceChangeReqManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERVICE_CHANGE_REQ>> Get(HisServiceChangeReqFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_CHANGE_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_CHANGE_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceChangeReqGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERVICE_CHANGE_REQ> Create(HisServiceChangeReqSDO data)
        {
            ApiResultObject<HIS_SERVICE_CHANGE_REQ> result = new ApiResultObject<HIS_SERVICE_CHANGE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_CHANGE_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisServiceChangeReqCreateSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_CHANGE_REQ> Approve(HisServiceChangeReqApproveSDO data)
        {
            ApiResultObject<HIS_SERVICE_CHANGE_REQ> result = new ApiResultObject<HIS_SERVICE_CHANGE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_CHANGE_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceChangeReqApprove(param).Run(data, ref resultData);
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
        public ApiResultObject<HisServiceChangeReqCashierApproveResultSDO> CashierApprove(HisServiceChangeReqCashierApproveSDO data)
        {
            ApiResultObject<HisServiceChangeReqCashierApproveResultSDO> result = new ApiResultObject<HisServiceChangeReqCashierApproveResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisServiceChangeReqCashierApproveResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceChangeReqCashierApprove(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_CHANGE_REQ> Update(HIS_SERVICE_CHANGE_REQ data)
        {
            ApiResultObject<HIS_SERVICE_CHANGE_REQ> result = new ApiResultObject<HIS_SERVICE_CHANGE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_CHANGE_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisServiceChangeReqUpdate(param).Update(data);
                    resultData = isSuccess ? data : null;
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
        public ApiResultObject<HIS_SERVICE_CHANGE_REQ> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERVICE_CHANGE_REQ> result = new ApiResultObject<HIS_SERVICE_CHANGE_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_CHANGE_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceChangeReqLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_CHANGE_REQ> Lock(long id)
        {
            ApiResultObject<HIS_SERVICE_CHANGE_REQ> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_CHANGE_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceChangeReqLock(param).Lock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_SERVICE_CHANGE_REQ> Unlock(long id)
        {
            ApiResultObject<HIS_SERVICE_CHANGE_REQ> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_CHANGE_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceChangeReqLock(param).Unlock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

		[Logger]
        public ApiResultObject<bool> Delete(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceChangeReqTruncate(param).Truncate(id);
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
