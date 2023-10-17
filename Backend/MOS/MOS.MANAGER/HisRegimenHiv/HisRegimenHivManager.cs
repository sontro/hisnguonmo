using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRegimenHiv
{
    public partial class HisRegimenHivManager : BusinessBase
    {
        public HisRegimenHivManager()
            : base()
        {

        }
        
        public HisRegimenHivManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_REGIMEN_HIV>> Get(HisRegimenHivFilterQuery filter)
        {
            ApiResultObject<List<HIS_REGIMEN_HIV>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_REGIMEN_HIV> resultData = null;
                if (valid)
                {
                    resultData = new HisRegimenHivGet(param).Get(filter);
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
        public ApiResultObject<HIS_REGIMEN_HIV> Create(HIS_REGIMEN_HIV data)
        {
            ApiResultObject<HIS_REGIMEN_HIV> result = new ApiResultObject<HIS_REGIMEN_HIV>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REGIMEN_HIV resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRegimenHivCreate(param).Create(data);
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
        public ApiResultObject<HIS_REGIMEN_HIV> Update(HIS_REGIMEN_HIV data)
        {
            ApiResultObject<HIS_REGIMEN_HIV> result = new ApiResultObject<HIS_REGIMEN_HIV>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REGIMEN_HIV resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRegimenHivUpdate(param).Update(data);
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
        public ApiResultObject<HIS_REGIMEN_HIV> ChangeLock(long id)
        {
            ApiResultObject<HIS_REGIMEN_HIV> result = new ApiResultObject<HIS_REGIMEN_HIV>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REGIMEN_HIV resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRegimenHivLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_REGIMEN_HIV> Lock(long id)
        {
            ApiResultObject<HIS_REGIMEN_HIV> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REGIMEN_HIV resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRegimenHivLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_REGIMEN_HIV> Unlock(long id)
        {
            ApiResultObject<HIS_REGIMEN_HIV> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REGIMEN_HIV resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRegimenHivLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRegimenHivTruncate(param).Truncate(id);
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
