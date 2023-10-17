using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuDetail
{
    public partial class HisSurgRemuDetailManager : BusinessBase
    {
        public HisSurgRemuDetailManager()
            : base()
        {

        }
        
        public HisSurgRemuDetailManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SURG_REMU_DETAIL>> Get(HisSurgRemuDetailFilterQuery filter)
        {
            ApiResultObject<List<HIS_SURG_REMU_DETAIL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SURG_REMU_DETAIL> resultData = null;
                if (valid)
                {
                    resultData = new HisSurgRemuDetailGet(param).Get(filter);
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
        public ApiResultObject<HIS_SURG_REMU_DETAIL> Create(HIS_SURG_REMU_DETAIL data)
        {
            ApiResultObject<HIS_SURG_REMU_DETAIL> result = new ApiResultObject<HIS_SURG_REMU_DETAIL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SURG_REMU_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSurgRemuDetailCreate(param).Create(data);
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
        public ApiResultObject<HIS_SURG_REMU_DETAIL> Update(HIS_SURG_REMU_DETAIL data)
        {
            ApiResultObject<HIS_SURG_REMU_DETAIL> result = new ApiResultObject<HIS_SURG_REMU_DETAIL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SURG_REMU_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSurgRemuDetailUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SURG_REMU_DETAIL> ChangeLock(long id)
        {
            ApiResultObject<HIS_SURG_REMU_DETAIL> result = new ApiResultObject<HIS_SURG_REMU_DETAIL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SURG_REMU_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSurgRemuDetailLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SURG_REMU_DETAIL> Lock(long id)
        {
            ApiResultObject<HIS_SURG_REMU_DETAIL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SURG_REMU_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSurgRemuDetailLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SURG_REMU_DETAIL> Unlock(long id)
        {
            ApiResultObject<HIS_SURG_REMU_DETAIL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SURG_REMU_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSurgRemuDetailLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSurgRemuDetailTruncate(param).Truncate(id);
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
