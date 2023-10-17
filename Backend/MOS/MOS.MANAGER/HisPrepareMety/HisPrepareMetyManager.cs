using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepareMety
{
    public partial class HisPrepareMetyManager : BusinessBase
    {
        public HisPrepareMetyManager()
            : base()
        {

        }
        
        public HisPrepareMetyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PREPARE_METY>> Get(HisPrepareMetyFilterQuery filter)
        {
            ApiResultObject<List<HIS_PREPARE_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PREPARE_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisPrepareMetyGet(param).Get(filter);
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
        public ApiResultObject<HIS_PREPARE_METY> Create(HIS_PREPARE_METY data)
        {
            ApiResultObject<HIS_PREPARE_METY> result = new ApiResultObject<HIS_PREPARE_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PREPARE_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPrepareMetyCreate(param).Create(data);
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
        public ApiResultObject<HIS_PREPARE_METY> Update(HIS_PREPARE_METY data)
        {
            ApiResultObject<HIS_PREPARE_METY> result = new ApiResultObject<HIS_PREPARE_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PREPARE_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPrepareMetyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PREPARE_METY> ChangeLock(long id)
        {
            ApiResultObject<HIS_PREPARE_METY> result = new ApiResultObject<HIS_PREPARE_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PREPARE_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareMetyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PREPARE_METY> Lock(long id)
        {
            ApiResultObject<HIS_PREPARE_METY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PREPARE_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareMetyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PREPARE_METY> Unlock(long id)
        {
            ApiResultObject<HIS_PREPARE_METY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PREPARE_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPrepareMetyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPrepareMetyTruncate(param).Truncate(id);
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
