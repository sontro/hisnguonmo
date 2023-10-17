using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBodyPart
{
    public partial class HisBodyPartManager : BusinessBase
    {
        public HisBodyPartManager()
            : base()
        {

        }
        
        public HisBodyPartManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BODY_PART>> Get(HisBodyPartFilterQuery filter)
        {
            ApiResultObject<List<HIS_BODY_PART>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BODY_PART> resultData = null;
                if (valid)
                {
                    resultData = new HisBodyPartGet(param).Get(filter);
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
        public ApiResultObject<HIS_BODY_PART> Create(HIS_BODY_PART data)
        {
            ApiResultObject<HIS_BODY_PART> result = new ApiResultObject<HIS_BODY_PART>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BODY_PART resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBodyPartCreate(param).Create(data);
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
        public ApiResultObject<HIS_BODY_PART> Update(HIS_BODY_PART data)
        {
            ApiResultObject<HIS_BODY_PART> result = new ApiResultObject<HIS_BODY_PART>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BODY_PART resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBodyPartUpdate(param).Update(data);
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
        public ApiResultObject<HIS_BODY_PART> ChangeLock(long id)
        {
            ApiResultObject<HIS_BODY_PART> result = new ApiResultObject<HIS_BODY_PART>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BODY_PART resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBodyPartLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BODY_PART> Lock(long id)
        {
            ApiResultObject<HIS_BODY_PART> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BODY_PART resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBodyPartLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BODY_PART> Unlock(long id)
        {
            ApiResultObject<HIS_BODY_PART> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BODY_PART resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBodyPartLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBodyPartTruncate(param).Truncate(id);
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
