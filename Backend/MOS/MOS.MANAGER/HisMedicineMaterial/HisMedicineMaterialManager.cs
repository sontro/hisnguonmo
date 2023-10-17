using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineMaterial
{
    public partial class HisMedicineMaterialManager : BusinessBase
    {
        public HisMedicineMaterialManager()
            : base()
        {

        }
        
        public HisMedicineMaterialManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDICINE_MATERIAL>> Get(HisMedicineMaterialFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICINE_MATERIAL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICINE_MATERIAL> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineMaterialGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDICINE_MATERIAL> Create(HIS_MEDICINE_MATERIAL data)
        {
            ApiResultObject<HIS_MEDICINE_MATERIAL> result = new ApiResultObject<HIS_MEDICINE_MATERIAL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_MATERIAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMedicineMaterialCreate(param).Create(data);
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
        public ApiResultObject<HIS_MEDICINE_MATERIAL> Update(HIS_MEDICINE_MATERIAL data)
        {
            ApiResultObject<HIS_MEDICINE_MATERIAL> result = new ApiResultObject<HIS_MEDICINE_MATERIAL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_MATERIAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMedicineMaterialUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MEDICINE_MATERIAL> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDICINE_MATERIAL> result = new ApiResultObject<HIS_MEDICINE_MATERIAL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_MATERIAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineMaterialLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICINE_MATERIAL> Lock(long id)
        {
            ApiResultObject<HIS_MEDICINE_MATERIAL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_MATERIAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineMaterialLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICINE_MATERIAL> Unlock(long id)
        {
            ApiResultObject<HIS_MEDICINE_MATERIAL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_MATERIAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineMaterialLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMedicineMaterialTruncate(param).Truncate(id);
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
