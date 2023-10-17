using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNextTreaIntr
{
    public partial class HisNextTreaIntrManager : BusinessBase
    {
        public HisNextTreaIntrManager()
            : base()
        {

        }
        
        public HisNextTreaIntrManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_NEXT_TREA_INTR>> Get(HisNextTreaIntrFilterQuery filter)
        {
            ApiResultObject<List<HIS_NEXT_TREA_INTR>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_NEXT_TREA_INTR> resultData = null;
                if (valid)
                {
                    resultData = new HisNextTreaIntrGet(param).Get(filter);
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
        public ApiResultObject<HIS_NEXT_TREA_INTR> Create(HIS_NEXT_TREA_INTR data)
        {
            ApiResultObject<HIS_NEXT_TREA_INTR> result = new ApiResultObject<HIS_NEXT_TREA_INTR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_NEXT_TREA_INTR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisNextTreaIntrCreate(param).Create(data);
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
        public ApiResultObject<HIS_NEXT_TREA_INTR> Update(HIS_NEXT_TREA_INTR data)
        {
            ApiResultObject<HIS_NEXT_TREA_INTR> result = new ApiResultObject<HIS_NEXT_TREA_INTR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_NEXT_TREA_INTR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisNextTreaIntrUpdate(param).Update(data);
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
        public ApiResultObject<HIS_NEXT_TREA_INTR> ChangeLock(long id)
        {
            ApiResultObject<HIS_NEXT_TREA_INTR> result = new ApiResultObject<HIS_NEXT_TREA_INTR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_NEXT_TREA_INTR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisNextTreaIntrLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_NEXT_TREA_INTR> Lock(long id)
        {
            ApiResultObject<HIS_NEXT_TREA_INTR> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_NEXT_TREA_INTR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisNextTreaIntrLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_NEXT_TREA_INTR> Unlock(long id)
        {
            ApiResultObject<HIS_NEXT_TREA_INTR> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_NEXT_TREA_INTR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisNextTreaIntrLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisNextTreaIntrTruncate(param).Truncate(id);
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
