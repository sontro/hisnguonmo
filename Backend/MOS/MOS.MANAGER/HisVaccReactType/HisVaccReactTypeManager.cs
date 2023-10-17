using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccReactType
{
    public partial class HisVaccReactTypeManager : BusinessBase
    {
        public HisVaccReactTypeManager()
            : base()
        {

        }
        
        public HisVaccReactTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_VACC_REACT_TYPE>> Get(HisVaccReactTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_VACC_REACT_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VACC_REACT_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccReactTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_VACC_REACT_TYPE> Create(HIS_VACC_REACT_TYPE data)
        {
            ApiResultObject<HIS_VACC_REACT_TYPE> result = new ApiResultObject<HIS_VACC_REACT_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_REACT_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccReactTypeCreate(param).Create(data);
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
        public ApiResultObject<HIS_VACC_REACT_TYPE> Update(HIS_VACC_REACT_TYPE data)
        {
            ApiResultObject<HIS_VACC_REACT_TYPE> result = new ApiResultObject<HIS_VACC_REACT_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_REACT_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccReactTypeUpdate(param).Update(data);
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
        public ApiResultObject<HIS_VACC_REACT_TYPE> ChangeLock(long id)
        {
            ApiResultObject<HIS_VACC_REACT_TYPE> result = new ApiResultObject<HIS_VACC_REACT_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_REACT_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccReactTypeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VACC_REACT_TYPE> Lock(long id)
        {
            ApiResultObject<HIS_VACC_REACT_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_REACT_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccReactTypeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VACC_REACT_TYPE> Unlock(long id)
        {
            ApiResultObject<HIS_VACC_REACT_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_REACT_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccReactTypeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisVaccReactTypeTruncate(param).Truncate(id);
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
