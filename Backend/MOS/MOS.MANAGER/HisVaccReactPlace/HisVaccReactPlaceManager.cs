using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccReactPlace
{
    public partial class HisVaccReactPlaceManager : BusinessBase
    {
        public HisVaccReactPlaceManager()
            : base()
        {

        }
        
        public HisVaccReactPlaceManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_VACC_REACT_PLACE>> Get(HisVaccReactPlaceFilterQuery filter)
        {
            ApiResultObject<List<HIS_VACC_REACT_PLACE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VACC_REACT_PLACE> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccReactPlaceGet(param).Get(filter);
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
        public ApiResultObject<HIS_VACC_REACT_PLACE> Create(HIS_VACC_REACT_PLACE data)
        {
            ApiResultObject<HIS_VACC_REACT_PLACE> result = new ApiResultObject<HIS_VACC_REACT_PLACE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_REACT_PLACE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccReactPlaceCreate(param).Create(data);
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
        public ApiResultObject<HIS_VACC_REACT_PLACE> Update(HIS_VACC_REACT_PLACE data)
        {
            ApiResultObject<HIS_VACC_REACT_PLACE> result = new ApiResultObject<HIS_VACC_REACT_PLACE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_REACT_PLACE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccReactPlaceUpdate(param).Update(data);
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
        public ApiResultObject<HIS_VACC_REACT_PLACE> ChangeLock(long id)
        {
            ApiResultObject<HIS_VACC_REACT_PLACE> result = new ApiResultObject<HIS_VACC_REACT_PLACE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_REACT_PLACE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccReactPlaceLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VACC_REACT_PLACE> Lock(long id)
        {
            ApiResultObject<HIS_VACC_REACT_PLACE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_REACT_PLACE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccReactPlaceLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VACC_REACT_PLACE> Unlock(long id)
        {
            ApiResultObject<HIS_VACC_REACT_PLACE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_REACT_PLACE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccReactPlaceLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisVaccReactPlaceTruncate(param).Truncate(id);
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
