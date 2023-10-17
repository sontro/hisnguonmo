using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationVrty
{
    public partial class HisVaccinationVrtyManager : BusinessBase
    {
        public HisVaccinationVrtyManager()
            : base()
        {

        }
        
        public HisVaccinationVrtyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_VACCINATION_VRTY>> Get(HisVaccinationVrtyFilterQuery filter)
        {
            ApiResultObject<List<HIS_VACCINATION_VRTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VACCINATION_VRTY> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccinationVrtyGet(param).Get(filter);
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
        public ApiResultObject<HIS_VACCINATION_VRTY> Create(HIS_VACCINATION_VRTY data)
        {
            ApiResultObject<HIS_VACCINATION_VRTY> result = new ApiResultObject<HIS_VACCINATION_VRTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_VRTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccinationVrtyCreate(param).Create(data);
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
        public ApiResultObject<HIS_VACCINATION_VRTY> Update(HIS_VACCINATION_VRTY data)
        {
            ApiResultObject<HIS_VACCINATION_VRTY> result = new ApiResultObject<HIS_VACCINATION_VRTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_VRTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccinationVrtyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_VACCINATION_VRTY> ChangeLock(long id)
        {
            ApiResultObject<HIS_VACCINATION_VRTY> result = new ApiResultObject<HIS_VACCINATION_VRTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_VRTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationVrtyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION_VRTY> Lock(long id)
        {
            ApiResultObject<HIS_VACCINATION_VRTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_VRTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationVrtyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION_VRTY> Unlock(long id)
        {
            ApiResultObject<HIS_VACCINATION_VRTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_VRTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationVrtyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisVaccinationVrtyTruncate(param).Truncate(id);
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
