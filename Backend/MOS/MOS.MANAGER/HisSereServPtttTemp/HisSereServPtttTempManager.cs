using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServPtttTemp
{
    public partial class HisSereServPtttTempManager : BusinessBase
    {
        public HisSereServPtttTempManager()
            : base()
        {

        }
        
        public HisSereServPtttTempManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERE_SERV_PTTT_TEMP>> Get(HisSereServPtttTempFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERE_SERV_PTTT_TEMP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERE_SERV_PTTT_TEMP> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServPtttTempGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERE_SERV_PTTT_TEMP> Create(HIS_SERE_SERV_PTTT_TEMP data)
        {
            ApiResultObject<HIS_SERE_SERV_PTTT_TEMP> result = new ApiResultObject<HIS_SERE_SERV_PTTT_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_PTTT_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSereServPtttTempCreate(param).Create(data);
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
        public ApiResultObject<HIS_SERE_SERV_PTTT_TEMP> Update(HIS_SERE_SERV_PTTT_TEMP data)
        {
            ApiResultObject<HIS_SERE_SERV_PTTT_TEMP> result = new ApiResultObject<HIS_SERE_SERV_PTTT_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_PTTT_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSereServPtttTempUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SERE_SERV_PTTT_TEMP> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_PTTT_TEMP> result = new ApiResultObject<HIS_SERE_SERV_PTTT_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_PTTT_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServPtttTempLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV_PTTT_TEMP> Lock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_PTTT_TEMP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_PTTT_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServPtttTempLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV_PTTT_TEMP> Unlock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_PTTT_TEMP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_PTTT_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServPtttTempLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSereServPtttTempTruncate(param).Truncate(id);
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
