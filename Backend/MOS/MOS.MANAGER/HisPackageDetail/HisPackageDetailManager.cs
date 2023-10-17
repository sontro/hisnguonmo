using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPackageDetail
{
    public partial class HisPackageDetailManager : BusinessBase
    {
        public HisPackageDetailManager()
            : base()
        {

        }
        
        public HisPackageDetailManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PACKAGE_DETAIL>> Get(HisPackageDetailFilterQuery filter)
        {
            ApiResultObject<List<HIS_PACKAGE_DETAIL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PACKAGE_DETAIL> resultData = null;
                if (valid)
                {
                    resultData = new HisPackageDetailGet(param).Get(filter);
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
        public ApiResultObject<HIS_PACKAGE_DETAIL> Create(HIS_PACKAGE_DETAIL data)
        {
            ApiResultObject<HIS_PACKAGE_DETAIL> result = new ApiResultObject<HIS_PACKAGE_DETAIL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PACKAGE_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPackageDetailCreate(param).Create(data);
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
        public ApiResultObject<HIS_PACKAGE_DETAIL> Update(HIS_PACKAGE_DETAIL data)
        {
            ApiResultObject<HIS_PACKAGE_DETAIL> result = new ApiResultObject<HIS_PACKAGE_DETAIL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PACKAGE_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPackageDetailUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PACKAGE_DETAIL> ChangeLock(long id)
        {
            ApiResultObject<HIS_PACKAGE_DETAIL> result = new ApiResultObject<HIS_PACKAGE_DETAIL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PACKAGE_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPackageDetailLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PACKAGE_DETAIL> Lock(long id)
        {
            ApiResultObject<HIS_PACKAGE_DETAIL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PACKAGE_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPackageDetailLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PACKAGE_DETAIL> Unlock(long id)
        {
            ApiResultObject<HIS_PACKAGE_DETAIL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PACKAGE_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPackageDetailLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPackageDetailTruncate(param).Truncate(id);
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
