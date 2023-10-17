using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVareVart
{
    public partial class HisVareVartManager : BusinessBase
    {
        public HisVareVartManager()
            : base()
        {

        }
        
        public HisVareVartManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_VARE_VART>> Get(HisVareVartFilterQuery filter)
        {
            ApiResultObject<List<HIS_VARE_VART>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VARE_VART> resultData = null;
                if (valid)
                {
                    resultData = new HisVareVartGet(param).Get(filter);
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
        public ApiResultObject<HIS_VARE_VART> Create(HIS_VARE_VART data)
        {
            ApiResultObject<HIS_VARE_VART> result = new ApiResultObject<HIS_VARE_VART>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VARE_VART resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVareVartCreate(param).Create(data);
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
        public ApiResultObject<HIS_VARE_VART> Update(HIS_VARE_VART data)
        {
            ApiResultObject<HIS_VARE_VART> result = new ApiResultObject<HIS_VARE_VART>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VARE_VART resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVareVartUpdate(param).Update(data);
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
        public ApiResultObject<HIS_VARE_VART> ChangeLock(long id)
        {
            ApiResultObject<HIS_VARE_VART> result = new ApiResultObject<HIS_VARE_VART>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VARE_VART resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVareVartLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VARE_VART> Lock(long id)
        {
            ApiResultObject<HIS_VARE_VART> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VARE_VART resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVareVartLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VARE_VART> Unlock(long id)
        {
            ApiResultObject<HIS_VARE_VART> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VARE_VART resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVareVartLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisVareVartTruncate(param).Truncate(id);
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
