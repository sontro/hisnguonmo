using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuneration
{
    public partial class HisSurgRemunerationManager : BusinessBase
    {
        public HisSurgRemunerationManager()
            : base()
        {

        }
        
        public HisSurgRemunerationManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SURG_REMUNERATION>> Get(HisSurgRemunerationFilterQuery filter)
        {
            ApiResultObject<List<HIS_SURG_REMUNERATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SURG_REMUNERATION> resultData = null;
                if (valid)
                {
                    resultData = new HisSurgRemunerationGet(param).Get(filter);
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
        public ApiResultObject<HIS_SURG_REMUNERATION> Create(HIS_SURG_REMUNERATION data)
        {
            ApiResultObject<HIS_SURG_REMUNERATION> result = new ApiResultObject<HIS_SURG_REMUNERATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SURG_REMUNERATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSurgRemunerationCreate(param).Create(data);
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
        public ApiResultObject<HIS_SURG_REMUNERATION> Update(HIS_SURG_REMUNERATION data)
        {
            ApiResultObject<HIS_SURG_REMUNERATION> result = new ApiResultObject<HIS_SURG_REMUNERATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SURG_REMUNERATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSurgRemunerationUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SURG_REMUNERATION> ChangeLock(long id)
        {
            ApiResultObject<HIS_SURG_REMUNERATION> result = new ApiResultObject<HIS_SURG_REMUNERATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SURG_REMUNERATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSurgRemunerationLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SURG_REMUNERATION> Lock(long id)
        {
            ApiResultObject<HIS_SURG_REMUNERATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SURG_REMUNERATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSurgRemunerationLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SURG_REMUNERATION> Unlock(long id)
        {
            ApiResultObject<HIS_SURG_REMUNERATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SURG_REMUNERATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSurgRemunerationLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSurgRemunerationTruncate(param).Truncate(id);
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
