using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationVrpl
{
    public partial class HisVaccinationVrplManager : BusinessBase
    {
        public HisVaccinationVrplManager()
            : base()
        {

        }
        
        public HisVaccinationVrplManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_VACCINATION_VRPL>> Get(HisVaccinationVrplFilterQuery filter)
        {
            ApiResultObject<List<HIS_VACCINATION_VRPL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VACCINATION_VRPL> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccinationVrplGet(param).Get(filter);
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
        public ApiResultObject<HIS_VACCINATION_VRPL> Create(HIS_VACCINATION_VRPL data)
        {
            ApiResultObject<HIS_VACCINATION_VRPL> result = new ApiResultObject<HIS_VACCINATION_VRPL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_VRPL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccinationVrplCreate(param).Create(data);
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
        public ApiResultObject<HIS_VACCINATION_VRPL> Update(HIS_VACCINATION_VRPL data)
        {
            ApiResultObject<HIS_VACCINATION_VRPL> result = new ApiResultObject<HIS_VACCINATION_VRPL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_VRPL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccinationVrplUpdate(param).Update(data);
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
        public ApiResultObject<HIS_VACCINATION_VRPL> ChangeLock(long id)
        {
            ApiResultObject<HIS_VACCINATION_VRPL> result = new ApiResultObject<HIS_VACCINATION_VRPL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_VRPL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationVrplLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION_VRPL> Lock(long id)
        {
            ApiResultObject<HIS_VACCINATION_VRPL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_VRPL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationVrplLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION_VRPL> Unlock(long id)
        {
            ApiResultObject<HIS_VACCINATION_VRPL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_VRPL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationVrplLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisVaccinationVrplTruncate(param).Truncate(id);
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
