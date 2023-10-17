using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMatyMaty
{
    public partial class HisMatyMatyManager : BusinessBase
    {
        public HisMatyMatyManager()
            : base()
        {

        }
        
        public HisMatyMatyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MATY_MATY>> Get(HisMatyMatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_MATY_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MATY_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMatyMatyGet(param).Get(filter);
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
        public ApiResultObject<HIS_MATY_MATY> Create(HIS_MATY_MATY data)
        {
            ApiResultObject<HIS_MATY_MATY> result = new ApiResultObject<HIS_MATY_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATY_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMatyMatyCreate(param).Create(data);
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
        public ApiResultObject<HIS_MATY_MATY> Update(HIS_MATY_MATY data)
        {
            ApiResultObject<HIS_MATY_MATY> result = new ApiResultObject<HIS_MATY_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATY_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMatyMatyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MATY_MATY> ChangeLock(long id)
        {
            ApiResultObject<HIS_MATY_MATY> result = new ApiResultObject<HIS_MATY_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MATY_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMatyMatyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MATY_MATY> Lock(long id)
        {
            ApiResultObject<HIS_MATY_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MATY_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMatyMatyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MATY_MATY> Unlock(long id)
        {
            ApiResultObject<HIS_MATY_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MATY_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMatyMatyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMatyMatyTruncate(param).Truncate(id);
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

        [Logger]
        public ApiResultObject<List<HIS_MATY_MATY>> CreateList(List<HIS_MATY_MATY> listData)
        {
            ApiResultObject<List<HIS_MATY_MATY>> result = new ApiResultObject<List<HIS_MATY_MATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_MATY_MATY> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMatyMatyCreate(param).CreateList(listData);
                    resultData = isSuccess ? listData : null;
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
        public ApiResultObject<List<HIS_MATY_MATY>> UpdateList(List<HIS_MATY_MATY> listData)
        {
            ApiResultObject<List<HIS_MATY_MATY>> result = new ApiResultObject<List<HIS_MATY_MATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_MATY_MATY> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMatyMatyUpdate(param).UpdateList(listData);
                    resultData = isSuccess ? listData : null;
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
        public ApiResultObject<bool> DeleteList(List<long> ids)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMatyMatyTruncate(param).TruncateList(ids);
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
