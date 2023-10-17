using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServRation
{
    public partial class HisSereServRationManager : BusinessBase
    {
        public HisSereServRationManager()
            : base()
        {

        }
        
        public HisSereServRationManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERE_SERV_RATION>> Get(HisSereServRationFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERE_SERV_RATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERE_SERV_RATION> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServRationGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERE_SERV_RATION> Create(HIS_SERE_SERV_RATION data)
        {
            ApiResultObject<HIS_SERE_SERV_RATION> result = new ApiResultObject<HIS_SERE_SERV_RATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_RATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSereServRationCreate(param).Create(data);
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
        public ApiResultObject<HIS_SERE_SERV_RATION> Update(HIS_SERE_SERV_RATION data)
        {
            ApiResultObject<HIS_SERE_SERV_RATION> result = new ApiResultObject<HIS_SERE_SERV_RATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_RATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSereServRationUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SERE_SERV_RATION> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_RATION> result = new ApiResultObject<HIS_SERE_SERV_RATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_RATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServRationLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV_RATION> Lock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_RATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_RATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServRationLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV_RATION> Unlock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_RATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_RATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServRationLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSereServRationTruncate(param).Truncate(id);
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
