using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationReact
{
    public partial class HisVaccinationReactManager : BusinessBase
    {
        public HisVaccinationReactManager()
            : base()
        {

        }
        
        public HisVaccinationReactManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_VACCINATION_REACT>> Get(HisVaccinationReactFilterQuery filter)
        {
            ApiResultObject<List<HIS_VACCINATION_REACT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VACCINATION_REACT> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccinationReactGet(param).Get(filter);
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
        public ApiResultObject<HIS_VACCINATION_REACT> Create(HIS_VACCINATION_REACT data)
        {
            ApiResultObject<HIS_VACCINATION_REACT> result = new ApiResultObject<HIS_VACCINATION_REACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_REACT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccinationReactCreate(param).Create(data);
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
        public ApiResultObject<HIS_VACCINATION_REACT> Update(HIS_VACCINATION_REACT data)
        {
            ApiResultObject<HIS_VACCINATION_REACT> result = new ApiResultObject<HIS_VACCINATION_REACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_REACT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccinationReactUpdate(param).Update(data);
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
        public ApiResultObject<HIS_VACCINATION_REACT> ChangeLock(long id)
        {
            ApiResultObject<HIS_VACCINATION_REACT> result = new ApiResultObject<HIS_VACCINATION_REACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_REACT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationReactLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION_REACT> Lock(long id)
        {
            ApiResultObject<HIS_VACCINATION_REACT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_REACT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationReactLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION_REACT> Unlock(long id)
        {
            ApiResultObject<HIS_VACCINATION_REACT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_REACT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationReactLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisVaccinationReactTruncate(param).Truncate(id);
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
