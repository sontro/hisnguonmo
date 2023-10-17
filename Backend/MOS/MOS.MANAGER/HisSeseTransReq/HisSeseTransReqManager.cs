using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSeseTransReq
{
    public partial class HisSeseTransReqManager : BusinessBase
    {
        public HisSeseTransReqManager()
            : base()
        {

        }
        
        public HisSeseTransReqManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SESE_TRANS_REQ>> Get(HisSeseTransReqFilterQuery filter)
        {
            ApiResultObject<List<HIS_SESE_TRANS_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SESE_TRANS_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisSeseTransReqGet(param).Get(filter);
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
        public ApiResultObject<HIS_SESE_TRANS_REQ> Create(HIS_SESE_TRANS_REQ data)
        {
            ApiResultObject<HIS_SESE_TRANS_REQ> result = new ApiResultObject<HIS_SESE_TRANS_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SESE_TRANS_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSeseTransReqCreate(param).Create(data);
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
        public ApiResultObject<HIS_SESE_TRANS_REQ> Update(HIS_SESE_TRANS_REQ data)
        {
            ApiResultObject<HIS_SESE_TRANS_REQ> result = new ApiResultObject<HIS_SESE_TRANS_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SESE_TRANS_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSeseTransReqUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SESE_TRANS_REQ> ChangeLock(long id)
        {
            ApiResultObject<HIS_SESE_TRANS_REQ> result = new ApiResultObject<HIS_SESE_TRANS_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SESE_TRANS_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSeseTransReqLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SESE_TRANS_REQ> Lock(long id)
        {
            ApiResultObject<HIS_SESE_TRANS_REQ> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SESE_TRANS_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSeseTransReqLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SESE_TRANS_REQ> Unlock(long id)
        {
            ApiResultObject<HIS_SESE_TRANS_REQ> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SESE_TRANS_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSeseTransReqLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSeseTransReqTruncate(param).Truncate(id);
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
