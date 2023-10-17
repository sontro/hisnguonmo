using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestReason
{
    public partial class HisExpMestReasonManager : BusinessBase
    {
        public HisExpMestReasonManager()
            : base()
        {

        }
        
        public HisExpMestReasonManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EXP_MEST_REASON>> Get(HisExpMestReasonFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXP_MEST_REASON>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXP_MEST_REASON> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestReasonGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXP_MEST_REASON> Create(HIS_EXP_MEST_REASON data)
        {
            ApiResultObject<HIS_EXP_MEST_REASON> result = new ApiResultObject<HIS_EXP_MEST_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_REASON resultData = null;
                if (valid && new HisExpMestReasonCreate(param).Create(data))
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
        public ApiResultObject<HIS_EXP_MEST_REASON> Update(HIS_EXP_MEST_REASON data)
        {
            ApiResultObject<HIS_EXP_MEST_REASON> result = new ApiResultObject<HIS_EXP_MEST_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_REASON resultData = null;
                if (valid && new HisExpMestReasonUpdate(param).Update(data))
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
        public ApiResultObject<HIS_EXP_MEST_REASON> ChangeLock(long id)
        {
            ApiResultObject<HIS_EXP_MEST_REASON> result = new ApiResultObject<HIS_EXP_MEST_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_REASON resultData = null;
                if (valid)
                {
                    new HisExpMestReasonLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST_REASON> Lock(long id)
        {
            ApiResultObject<HIS_EXP_MEST_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_REASON resultData = null;
                if (valid)
                {
                    new HisExpMestReasonLock(param).Lock(id, ref resultData);
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
		
		[Logger]
        public ApiResultObject<HIS_EXP_MEST_REASON> Unlock(long id)
        {
            ApiResultObject<HIS_EXP_MEST_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_REASON resultData = null;
                if (valid)
                {
                    new HisExpMestReasonLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisExpMestReasonTruncate(param).Truncate(id);
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
