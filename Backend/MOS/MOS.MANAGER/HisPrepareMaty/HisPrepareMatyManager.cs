using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepareMaty
{
    public partial class HisPrepareMatyManager : BusinessBase
    {
        public HisPrepareMatyManager()
            : base()
        {

        }
        
        public HisPrepareMatyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PREPARE_MATY>> Get(HisPrepareMatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_PREPARE_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PREPARE_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisPrepareMatyGet(param).Get(filter);
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
        public ApiResultObject<HIS_PREPARE_MATY> Create(HIS_PREPARE_MATY data)
        {
            ApiResultObject<HIS_PREPARE_MATY> result = new ApiResultObject<HIS_PREPARE_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PREPARE_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPrepareMatyCreate(param).Create(data);
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
        public ApiResultObject<HIS_PREPARE_MATY> Update(HIS_PREPARE_MATY data)
        {
            ApiResultObject<HIS_PREPARE_MATY> result = new ApiResultObject<HIS_PREPARE_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PREPARE_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPrepareMatyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PREPARE_MATY> ChangeLock(long id)
        {
            ApiResultObject<HIS_PREPARE_MATY> result = new ApiResultObject<HIS_PREPARE_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PREPARE_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareMatyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PREPARE_MATY> Lock(long id)
        {
            ApiResultObject<HIS_PREPARE_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PREPARE_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareMatyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PREPARE_MATY> Unlock(long id)
        {
            ApiResultObject<HIS_PREPARE_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PREPARE_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareMatyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPrepareMatyTruncate(param).Truncate(id);
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
