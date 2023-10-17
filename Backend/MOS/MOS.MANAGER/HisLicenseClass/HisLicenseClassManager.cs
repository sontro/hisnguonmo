using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisLicenseClass
{
    public partial class HisLicenseClassManager : BusinessBase
    {
        public HisLicenseClassManager()
            : base()
        {

        }
        
        public HisLicenseClassManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_LICENSE_CLASS>> Get(HisLicenseClassFilterQuery filter)
        {
            ApiResultObject<List<HIS_LICENSE_CLASS>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_LICENSE_CLASS> resultData = null;
                if (valid)
                {
                    resultData = new HisLicenseClassGet(param).Get(filter);
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
        public ApiResultObject<HIS_LICENSE_CLASS> Create(HIS_LICENSE_CLASS data)
        {
            ApiResultObject<HIS_LICENSE_CLASS> result = new ApiResultObject<HIS_LICENSE_CLASS>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_LICENSE_CLASS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisLicenseClassCreate(param).Create(data);
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
        public ApiResultObject<HIS_LICENSE_CLASS> Update(HIS_LICENSE_CLASS data)
        {
            ApiResultObject<HIS_LICENSE_CLASS> result = new ApiResultObject<HIS_LICENSE_CLASS>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_LICENSE_CLASS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisLicenseClassUpdate(param).Update(data);
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
        public ApiResultObject<HIS_LICENSE_CLASS> ChangeLock(long id)
        {
            ApiResultObject<HIS_LICENSE_CLASS> result = new ApiResultObject<HIS_LICENSE_CLASS>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_LICENSE_CLASS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisLicenseClassLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_LICENSE_CLASS> Lock(long id)
        {
            ApiResultObject<HIS_LICENSE_CLASS> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_LICENSE_CLASS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisLicenseClassLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_LICENSE_CLASS> Unlock(long id)
        {
            ApiResultObject<HIS_LICENSE_CLASS> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_LICENSE_CLASS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisLicenseClassLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisLicenseClassTruncate(param).Truncate(id);
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
