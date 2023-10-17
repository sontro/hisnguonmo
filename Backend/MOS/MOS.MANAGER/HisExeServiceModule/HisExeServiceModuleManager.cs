using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExeServiceModule
{
    public partial class HisExeServiceModuleManager : BusinessBase
    {
        public HisExeServiceModuleManager()
            : base()
        {

        }
        
        public HisExeServiceModuleManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EXE_SERVICE_MODULE>> Get(HisExeServiceModuleFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXE_SERVICE_MODULE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXE_SERVICE_MODULE> resultData = null;
                if (valid)
                {
                    resultData = new HisExeServiceModuleGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXE_SERVICE_MODULE> Create(HIS_EXE_SERVICE_MODULE data)
        {
            ApiResultObject<HIS_EXE_SERVICE_MODULE> result = new ApiResultObject<HIS_EXE_SERVICE_MODULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXE_SERVICE_MODULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisExeServiceModuleCreate(param).Create(data);
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
        public ApiResultObject<HIS_EXE_SERVICE_MODULE> Update(HIS_EXE_SERVICE_MODULE data)
        {
            ApiResultObject<HIS_EXE_SERVICE_MODULE> result = new ApiResultObject<HIS_EXE_SERVICE_MODULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXE_SERVICE_MODULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisExeServiceModuleUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EXE_SERVICE_MODULE> ChangeLock(long id)
        {
            ApiResultObject<HIS_EXE_SERVICE_MODULE> result = new ApiResultObject<HIS_EXE_SERVICE_MODULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXE_SERVICE_MODULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExeServiceModuleLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EXE_SERVICE_MODULE> Lock(long id)
        {
            ApiResultObject<HIS_EXE_SERVICE_MODULE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXE_SERVICE_MODULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExeServiceModuleLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EXE_SERVICE_MODULE> Unlock(long id)
        {
            ApiResultObject<HIS_EXE_SERVICE_MODULE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXE_SERVICE_MODULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExeServiceModuleLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisExeServiceModuleTruncate(param).Truncate(id);
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
