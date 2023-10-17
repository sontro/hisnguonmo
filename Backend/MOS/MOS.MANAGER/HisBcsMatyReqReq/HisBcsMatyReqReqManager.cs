using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMatyReqReq
{
    public partial class HisBcsMatyReqReqManager : BusinessBase
    {
        public HisBcsMatyReqReqManager()
            : base()
        {

        }
        
        public HisBcsMatyReqReqManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BCS_MATY_REQ_REQ>> Get(HisBcsMatyReqReqFilterQuery filter)
        {
            ApiResultObject<List<HIS_BCS_MATY_REQ_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BCS_MATY_REQ_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisBcsMatyReqReqGet(param).Get(filter);
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
        public ApiResultObject<HIS_BCS_MATY_REQ_REQ> Create(HIS_BCS_MATY_REQ_REQ data)
        {
            ApiResultObject<HIS_BCS_MATY_REQ_REQ> result = new ApiResultObject<HIS_BCS_MATY_REQ_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BCS_MATY_REQ_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBcsMatyReqReqCreate(param).Create(data);
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
        public ApiResultObject<HIS_BCS_MATY_REQ_REQ> Update(HIS_BCS_MATY_REQ_REQ data)
        {
            ApiResultObject<HIS_BCS_MATY_REQ_REQ> result = new ApiResultObject<HIS_BCS_MATY_REQ_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BCS_MATY_REQ_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBcsMatyReqReqUpdate(param).Update(data);
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
        public ApiResultObject<HIS_BCS_MATY_REQ_REQ> ChangeLock(long id)
        {
            ApiResultObject<HIS_BCS_MATY_REQ_REQ> result = new ApiResultObject<HIS_BCS_MATY_REQ_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BCS_MATY_REQ_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBcsMatyReqReqLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BCS_MATY_REQ_REQ> Lock(long id)
        {
            ApiResultObject<HIS_BCS_MATY_REQ_REQ> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BCS_MATY_REQ_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBcsMatyReqReqLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BCS_MATY_REQ_REQ> Unlock(long id)
        {
            ApiResultObject<HIS_BCS_MATY_REQ_REQ> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BCS_MATY_REQ_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBcsMatyReqReqLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBcsMatyReqReqTruncate(param).Truncate(id);
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
