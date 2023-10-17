using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFilmSize
{
    public partial class HisFilmSizeManager : BusinessBase
    {
        public HisFilmSizeManager()
            : base()
        {

        }
        
        public HisFilmSizeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_FILM_SIZE>> Get(HisFilmSizeFilterQuery filter)
        {
            ApiResultObject<List<HIS_FILM_SIZE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_FILM_SIZE> resultData = null;
                if (valid)
                {
                    resultData = new HisFilmSizeGet(param).Get(filter);
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
        public ApiResultObject<HIS_FILM_SIZE> Create(HIS_FILM_SIZE data)
        {
            ApiResultObject<HIS_FILM_SIZE> result = new ApiResultObject<HIS_FILM_SIZE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_FILM_SIZE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisFilmSizeCreate(param).Create(data);
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
        public ApiResultObject<HIS_FILM_SIZE> Update(HIS_FILM_SIZE data)
        {
            ApiResultObject<HIS_FILM_SIZE> result = new ApiResultObject<HIS_FILM_SIZE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_FILM_SIZE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisFilmSizeUpdate(param).Update(data);
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
        public ApiResultObject<HIS_FILM_SIZE> ChangeLock(long id)
        {
            ApiResultObject<HIS_FILM_SIZE> result = new ApiResultObject<HIS_FILM_SIZE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FILM_SIZE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFilmSizeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_FILM_SIZE> Lock(long id)
        {
            ApiResultObject<HIS_FILM_SIZE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FILM_SIZE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFilmSizeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_FILM_SIZE> Unlock(long id)
        {
            ApiResultObject<HIS_FILM_SIZE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FILM_SIZE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFilmSizeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisFilmSizeTruncate(param).Truncate(id);
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
