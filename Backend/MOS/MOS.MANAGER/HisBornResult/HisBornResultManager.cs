using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBornResult
{
    public partial class HisBornResultManager : BusinessBase
    {
        public HisBornResultManager()
            : base()
        {

        }
        
        public HisBornResultManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BORN_RESULT>> Get(HisBornResultFilterQuery filter)
        {
            ApiResultObject<List<HIS_BORN_RESULT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BORN_RESULT> resultData = null;
                if (valid)
                {
                    resultData = new HisBornResultGet(param).Get(filter);
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
        public ApiResultObject<HIS_BORN_RESULT> Create(HIS_BORN_RESULT data)
        {
            ApiResultObject<HIS_BORN_RESULT> result = new ApiResultObject<HIS_BORN_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BORN_RESULT resultData = null;
                if (valid && new HisBornResultCreate(param).Create(data))
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
        public ApiResultObject<HIS_BORN_RESULT> Update(HIS_BORN_RESULT data)
        {
            ApiResultObject<HIS_BORN_RESULT> result = new ApiResultObject<HIS_BORN_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BORN_RESULT resultData = null;
                if (valid && new HisBornResultUpdate(param).Update(data))
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
        public ApiResultObject<HIS_BORN_RESULT> ChangeLock(long id)
        {
            ApiResultObject<HIS_BORN_RESULT> result = new ApiResultObject<HIS_BORN_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BORN_RESULT resultData = null;
                if (valid)
                {
                    new HisBornResultLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BORN_RESULT> Lock(long id)
        {
            ApiResultObject<HIS_BORN_RESULT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BORN_RESULT resultData = null;
                if (valid)
                {
                    new HisBornResultLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BORN_RESULT> Unlock(long id)
        {
            ApiResultObject<HIS_BORN_RESULT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BORN_RESULT resultData = null;
                if (valid)
                {
                    new HisBornResultLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBornResultTruncate(param).Truncate(id);
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
