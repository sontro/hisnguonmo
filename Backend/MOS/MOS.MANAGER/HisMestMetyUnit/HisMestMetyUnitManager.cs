using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMetyUnit
{
    public partial class HisMestMetyUnitManager : BusinessBase
    {
        public HisMestMetyUnitManager()
            : base()
        {

        }
        
        public HisMestMetyUnitManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEST_METY_UNIT>> Get(HisMestMetyUnitFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_METY_UNIT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_METY_UNIT> resultData = null;
                if (valid)
                {
                    resultData = new HisMestMetyUnitGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEST_METY_UNIT> Create(HIS_MEST_METY_UNIT data)
        {
            ApiResultObject<HIS_MEST_METY_UNIT> result = new ApiResultObject<HIS_MEST_METY_UNIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_METY_UNIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMestMetyUnitCreate(param).Create(data);
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
        public ApiResultObject<HIS_MEST_METY_UNIT> Update(HIS_MEST_METY_UNIT data)
        {
            ApiResultObject<HIS_MEST_METY_UNIT> result = new ApiResultObject<HIS_MEST_METY_UNIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_METY_UNIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMestMetyUnitUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MEST_METY_UNIT> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEST_METY_UNIT> result = new ApiResultObject<HIS_MEST_METY_UNIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_METY_UNIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMestMetyUnitLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_METY_UNIT> Lock(long id)
        {
            ApiResultObject<HIS_MEST_METY_UNIT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_METY_UNIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMestMetyUnitLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_METY_UNIT> Unlock(long id)
        {
            ApiResultObject<HIS_MEST_METY_UNIT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_METY_UNIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMestMetyUnitLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMestMetyUnitTruncate(param).Truncate(id);
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
