using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOccupational
{
    public partial class HisKskOccupationalManager : BusinessBase
    {
        public HisKskOccupationalManager()
            : base()
        {

        }
        
        public HisKskOccupationalManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_KSK_OCCUPATIONAL>> Get(HisKskOccupationalFilterQuery filter)
        {
            ApiResultObject<List<HIS_KSK_OCCUPATIONAL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_KSK_OCCUPATIONAL> resultData = null;
                if (valid)
                {
                    resultData = new HisKskOccupationalGet(param).Get(filter);
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
        public ApiResultObject<HIS_KSK_OCCUPATIONAL> Create(HIS_KSK_OCCUPATIONAL data)
        {
            ApiResultObject<HIS_KSK_OCCUPATIONAL> result = new ApiResultObject<HIS_KSK_OCCUPATIONAL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_OCCUPATIONAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskOccupationalCreate(param).Create(data);
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
        public ApiResultObject<HIS_KSK_OCCUPATIONAL> Update(HIS_KSK_OCCUPATIONAL data)
        {
            ApiResultObject<HIS_KSK_OCCUPATIONAL> result = new ApiResultObject<HIS_KSK_OCCUPATIONAL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_OCCUPATIONAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskOccupationalUpdate(param).Update(data);
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
        public ApiResultObject<HIS_KSK_OCCUPATIONAL> ChangeLock(long id)
        {
            ApiResultObject<HIS_KSK_OCCUPATIONAL> result = new ApiResultObject<HIS_KSK_OCCUPATIONAL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_OCCUPATIONAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskOccupationalLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_OCCUPATIONAL> Lock(long id)
        {
            ApiResultObject<HIS_KSK_OCCUPATIONAL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_OCCUPATIONAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskOccupationalLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_OCCUPATIONAL> Unlock(long id)
        {
            ApiResultObject<HIS_KSK_OCCUPATIONAL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_OCCUPATIONAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskOccupationalLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisKskOccupationalTruncate(param).Truncate(id);
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
