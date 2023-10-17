using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAllergenic
{
    public partial class HisAllergenicManager : BusinessBase
    {
        public HisAllergenicManager()
            : base()
        {

        }
        
        public HisAllergenicManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ALLERGENIC>> Get(HisAllergenicFilterQuery filter)
        {
            ApiResultObject<List<HIS_ALLERGENIC>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ALLERGENIC> resultData = null;
                if (valid)
                {
                    resultData = new HisAllergenicGet(param).Get(filter);
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
        public ApiResultObject<HIS_ALLERGENIC> Create(HIS_ALLERGENIC data)
        {
            ApiResultObject<HIS_ALLERGENIC> result = new ApiResultObject<HIS_ALLERGENIC>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ALLERGENIC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAllergenicCreate(param).Create(data);
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
        public ApiResultObject<HIS_ALLERGENIC> Update(HIS_ALLERGENIC data)
        {
            ApiResultObject<HIS_ALLERGENIC> result = new ApiResultObject<HIS_ALLERGENIC>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ALLERGENIC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAllergenicUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ALLERGENIC> ChangeLock(long id)
        {
            ApiResultObject<HIS_ALLERGENIC> result = new ApiResultObject<HIS_ALLERGENIC>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ALLERGENIC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAllergenicLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ALLERGENIC> Lock(long id)
        {
            ApiResultObject<HIS_ALLERGENIC> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ALLERGENIC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAllergenicLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ALLERGENIC> Unlock(long id)
        {
            ApiResultObject<HIS_ALLERGENIC> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ALLERGENIC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAllergenicLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAllergenicTruncate(param).Truncate(id);
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
