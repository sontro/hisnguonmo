using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreDhty
{
    public partial class HisHoreDhtyManager : BusinessBase
    {
        public HisHoreDhtyManager()
            : base()
        {

        }
        
        public HisHoreDhtyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_HORE_DHTY>> Get(HisHoreDhtyFilterQuery filter)
        {
            ApiResultObject<List<HIS_HORE_DHTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_HORE_DHTY> resultData = null;
                if (valid)
                {
                    resultData = new HisHoreDhtyGet(param).Get(filter);
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
        public ApiResultObject<HIS_HORE_DHTY> Create(HIS_HORE_DHTY data)
        {
            ApiResultObject<HIS_HORE_DHTY> result = new ApiResultObject<HIS_HORE_DHTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HORE_DHTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisHoreDhtyCreate(param).Create(data);
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
        public ApiResultObject<HIS_HORE_DHTY> Update(HIS_HORE_DHTY data)
        {
            ApiResultObject<HIS_HORE_DHTY> result = new ApiResultObject<HIS_HORE_DHTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HORE_DHTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisHoreDhtyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_HORE_DHTY> ChangeLock(long id)
        {
            ApiResultObject<HIS_HORE_DHTY> result = new ApiResultObject<HIS_HORE_DHTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HORE_DHTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreDhtyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_HORE_DHTY> Lock(long id)
        {
            ApiResultObject<HIS_HORE_DHTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HORE_DHTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreDhtyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_HORE_DHTY> Unlock(long id)
        {
            ApiResultObject<HIS_HORE_DHTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HORE_DHTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreDhtyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisHoreDhtyTruncate(param).Truncate(id);
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
