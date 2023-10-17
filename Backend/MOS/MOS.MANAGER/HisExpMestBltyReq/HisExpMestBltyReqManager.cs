using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestBltyReq
{
    public partial class HisExpMestBltyReqManager : BusinessBase
    {
        public HisExpMestBltyReqManager()
            : base()
        {

        }
        
        public HisExpMestBltyReqManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EXP_MEST_BLTY_REQ>> Get(HisExpMestBltyReqFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXP_MEST_BLTY_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXP_MEST_BLTY_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestBltyReqGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXP_MEST_BLTY_REQ> Create(HIS_EXP_MEST_BLTY_REQ data)
        {
            ApiResultObject<HIS_EXP_MEST_BLTY_REQ> result = new ApiResultObject<HIS_EXP_MEST_BLTY_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_BLTY_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisExpMestBltyReqCreate(param).Create(data);
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
        public ApiResultObject<HIS_EXP_MEST_BLTY_REQ> Update(HIS_EXP_MEST_BLTY_REQ data)
        {
            ApiResultObject<HIS_EXP_MEST_BLTY_REQ> result = new ApiResultObject<HIS_EXP_MEST_BLTY_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_BLTY_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisExpMestBltyReqUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EXP_MEST_BLTY_REQ> ChangeLock(long id)
        {
            ApiResultObject<HIS_EXP_MEST_BLTY_REQ> result = new ApiResultObject<HIS_EXP_MEST_BLTY_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_BLTY_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestBltyReqLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST_BLTY_REQ> Lock(long id)
        {
            ApiResultObject<HIS_EXP_MEST_BLTY_REQ> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_BLTY_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestBltyReqLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST_BLTY_REQ> Unlock(long id)
        {
            ApiResultObject<HIS_EXP_MEST_BLTY_REQ> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_BLTY_REQ resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestBltyReqLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisExpMestBltyReqTruncate(param).Truncate(id);
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
