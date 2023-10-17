using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepositReq
{
    public partial class HisDepositReqManager : BusinessBase
    {
        public HisDepositReqManager()
            : base()
        {

        }
        
        public HisDepositReqManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_DEPOSIT_REQ>> Get(HisDepositReqFilterQuery filter)
        {
            ApiResultObject<List<HIS_DEPOSIT_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DEPOSIT_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisDepositReqGet(param).Get(filter);
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
        public ApiResultObject<HIS_DEPOSIT_REQ> Create(HIS_DEPOSIT_REQ data)
        {
            ApiResultObject<HIS_DEPOSIT_REQ> result = new ApiResultObject<HIS_DEPOSIT_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEPOSIT_REQ resultData = null;
                if (valid && new HisDepositReqCreate(param).Create(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_DEPOSIT_REQ> Update(HIS_DEPOSIT_REQ data)
        {
            ApiResultObject<HIS_DEPOSIT_REQ> result = new ApiResultObject<HIS_DEPOSIT_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEPOSIT_REQ resultData = null;
                if (valid && new HisDepositReqUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_DEPOSIT_REQ> ChangeLock(HIS_DEPOSIT_REQ data)
        {
            ApiResultObject<HIS_DEPOSIT_REQ> result = new ApiResultObject<HIS_DEPOSIT_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEPOSIT_REQ resultData = null;
                if (valid && new HisDepositReqLock(param).ChangeLock(data))
                {
                    resultData = data;
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
                    resultData = new HisDepositReqTruncate(param).Truncate(id);
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
