using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpBltyService
{
    public partial class HisExpBltyServiceManager : BusinessBase
    {
        public HisExpBltyServiceManager()
            : base()
        {

        }
        
        public HisExpBltyServiceManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EXP_BLTY_SERVICE>> Get(HisExpBltyServiceFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXP_BLTY_SERVICE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXP_BLTY_SERVICE> resultData = null;
                if (valid)
                {
                    resultData = new HisExpBltyServiceGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXP_BLTY_SERVICE> Create(HIS_EXP_BLTY_SERVICE data)
        {
            ApiResultObject<HIS_EXP_BLTY_SERVICE> result = new ApiResultObject<HIS_EXP_BLTY_SERVICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_BLTY_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisExpBltyServiceCreate(param).Create(data);
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
        public ApiResultObject<HIS_EXP_BLTY_SERVICE> Update(HIS_EXP_BLTY_SERVICE data)
        {
            ApiResultObject<HIS_EXP_BLTY_SERVICE> result = new ApiResultObject<HIS_EXP_BLTY_SERVICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_BLTY_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisExpBltyServiceUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EXP_BLTY_SERVICE> ChangeLock(long id)
        {
            ApiResultObject<HIS_EXP_BLTY_SERVICE> result = new ApiResultObject<HIS_EXP_BLTY_SERVICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_BLTY_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpBltyServiceLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EXP_BLTY_SERVICE> Lock(long id)
        {
            ApiResultObject<HIS_EXP_BLTY_SERVICE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_BLTY_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpBltyServiceLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EXP_BLTY_SERVICE> Unlock(long id)
        {
            ApiResultObject<HIS_EXP_BLTY_SERVICE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_BLTY_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpBltyServiceLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisExpBltyServiceTruncate(param).Truncate(id);
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
