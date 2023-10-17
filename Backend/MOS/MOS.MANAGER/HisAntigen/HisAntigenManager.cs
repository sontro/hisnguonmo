using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigen
{
    public partial class HisAntigenManager : BusinessBase
    {
        public HisAntigenManager()
            : base()
        {

        }
        
        public HisAntigenManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ANTIGEN>> Get(HisAntigenFilterQuery filter)
        {
            ApiResultObject<List<HIS_ANTIGEN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ANTIGEN> resultData = null;
                if (valid)
                {
                    resultData = new HisAntigenGet(param).Get(filter);
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
        public ApiResultObject<HIS_ANTIGEN> Create(HIS_ANTIGEN data)
        {
            ApiResultObject<HIS_ANTIGEN> result = new ApiResultObject<HIS_ANTIGEN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIGEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAntigenCreate(param).Create(data);
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
        public ApiResultObject<HIS_ANTIGEN> Update(HIS_ANTIGEN data)
        {
            ApiResultObject<HIS_ANTIGEN> result = new ApiResultObject<HIS_ANTIGEN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIGEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAntigenUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ANTIGEN> ChangeLock(long id)
        {
            ApiResultObject<HIS_ANTIGEN> result = new ApiResultObject<HIS_ANTIGEN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIGEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntigenLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTIGEN> Lock(long id)
        {
            ApiResultObject<HIS_ANTIGEN> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIGEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntigenLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTIGEN> Unlock(long id)
        {
            ApiResultObject<HIS_ANTIGEN> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIGEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntigenLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAntigenTruncate(param).Truncate(id);
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
