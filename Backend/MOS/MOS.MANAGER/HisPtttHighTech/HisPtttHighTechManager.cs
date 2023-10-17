using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttHighTech
{
    public partial class HisPtttHighTechManager : BusinessBase
    {
        public HisPtttHighTechManager()
            : base()
        {

        }
        
        public HisPtttHighTechManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PTTT_HIGH_TECH>> Get(HisPtttHighTechFilterQuery filter)
        {
            ApiResultObject<List<HIS_PTTT_HIGH_TECH>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PTTT_HIGH_TECH> resultData = null;
                if (valid)
                {
                    resultData = new HisPtttHighTechGet(param).Get(filter);
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
        public ApiResultObject<HIS_PTTT_HIGH_TECH> Create(HIS_PTTT_HIGH_TECH data)
        {
            ApiResultObject<HIS_PTTT_HIGH_TECH> result = new ApiResultObject<HIS_PTTT_HIGH_TECH>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_HIGH_TECH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPtttHighTechCreate(param).Create(data);
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
        public ApiResultObject<HIS_PTTT_HIGH_TECH> Update(HIS_PTTT_HIGH_TECH data)
        {
            ApiResultObject<HIS_PTTT_HIGH_TECH> result = new ApiResultObject<HIS_PTTT_HIGH_TECH>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_HIGH_TECH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPtttHighTechUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PTTT_HIGH_TECH> ChangeLock(long id)
        {
            ApiResultObject<HIS_PTTT_HIGH_TECH> result = new ApiResultObject<HIS_PTTT_HIGH_TECH>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_HIGH_TECH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttHighTechLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PTTT_HIGH_TECH> Lock(long id)
        {
            ApiResultObject<HIS_PTTT_HIGH_TECH> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_HIGH_TECH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttHighTechLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PTTT_HIGH_TECH> Unlock(long id)
        {
            ApiResultObject<HIS_PTTT_HIGH_TECH> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_HIGH_TECH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttHighTechLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPtttHighTechTruncate(param).Truncate(id);
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
