using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHoha
{
    public partial class HisHoreHohaManager : BusinessBase
    {
        public HisHoreHohaManager()
            : base()
        {

        }
        
        public HisHoreHohaManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_HORE_HOHA>> Get(HisHoreHohaFilterQuery filter)
        {
            ApiResultObject<List<HIS_HORE_HOHA>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_HORE_HOHA> resultData = null;
                if (valid)
                {
                    resultData = new HisHoreHohaGet(param).Get(filter);
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
        public ApiResultObject<HIS_HORE_HOHA> Create(HIS_HORE_HOHA data)
        {
            ApiResultObject<HIS_HORE_HOHA> result = new ApiResultObject<HIS_HORE_HOHA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HORE_HOHA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisHoreHohaCreate(param).Create(data);
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
        public ApiResultObject<HIS_HORE_HOHA> Update(HIS_HORE_HOHA data)
        {
            ApiResultObject<HIS_HORE_HOHA> result = new ApiResultObject<HIS_HORE_HOHA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HORE_HOHA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisHoreHohaUpdate(param).Update(data);
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
        public ApiResultObject<HIS_HORE_HOHA> ChangeLock(long id)
        {
            ApiResultObject<HIS_HORE_HOHA> result = new ApiResultObject<HIS_HORE_HOHA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HORE_HOHA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreHohaLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_HORE_HOHA> Lock(long id)
        {
            ApiResultObject<HIS_HORE_HOHA> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HORE_HOHA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreHohaLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_HORE_HOHA> Unlock(long id)
        {
            ApiResultObject<HIS_HORE_HOHA> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HORE_HOHA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreHohaLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisHoreHohaTruncate(param).Truncate(id);
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
