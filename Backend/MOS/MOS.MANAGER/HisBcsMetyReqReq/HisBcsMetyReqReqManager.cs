using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMetyReqReq
{
    public partial class HisBcsMetyReqReqManager : BusinessBase
    {
        public HisBcsMetyReqReqManager()
            : base()
        {

        }
        
        public HisBcsMetyReqReqManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BCS_METY_REQ_REQ>> Get(HisBcsMetyReqReqFilterQuery filter)
        {
            ApiResultObject<List<HIS_BCS_METY_REQ_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BCS_METY_REQ_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisBcsMetyReqReqGet(param).Get(filter);
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
        public ApiResultObject<HIS_BCS_METY_REQ_REQ> Create(HIS_BCS_METY_REQ_REQ data)
        {
            ApiResultObject<HIS_BCS_METY_REQ_REQ> result = new ApiResultObject<HIS_BCS_METY_REQ_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BCS_METY_REQ_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBcsMetyReqReqCreate(param).Create(data);
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
        public ApiResultObject<HIS_BCS_METY_REQ_REQ> Update(HIS_BCS_METY_REQ_REQ data)
        {
            ApiResultObject<HIS_BCS_METY_REQ_REQ> result = new ApiResultObject<HIS_BCS_METY_REQ_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BCS_METY_REQ_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBcsMetyReqReqUpdate(param).Update(data);
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
        public ApiResultObject<HIS_BCS_METY_REQ_REQ> ChangeLock(long id)
        {
            ApiResultObject<HIS_BCS_METY_REQ_REQ> result = new ApiResultObject<HIS_BCS_METY_REQ_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BCS_METY_REQ_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBcsMetyReqReqLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BCS_METY_REQ_REQ> Lock(long id)
        {
            ApiResultObject<HIS_BCS_METY_REQ_REQ> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BCS_METY_REQ_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBcsMetyReqReqLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BCS_METY_REQ_REQ> Unlock(long id)
        {
            ApiResultObject<HIS_BCS_METY_REQ_REQ> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BCS_METY_REQ_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBcsMetyReqReqLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBcsMetyReqReqTruncate(param).Truncate(id);
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
