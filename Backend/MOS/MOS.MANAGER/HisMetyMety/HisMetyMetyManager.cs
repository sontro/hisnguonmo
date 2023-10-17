using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyMety
{
    public partial class HisMetyMetyManager : BusinessBase
    {
        public HisMetyMetyManager()
            : base()
        {

        }
        
        public HisMetyMetyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_METY_METY>> Get(HisMetyMetyFilterQuery filter)
        {
            ApiResultObject<List<HIS_METY_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_METY_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisMetyMetyGet(param).Get(filter);
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
        public ApiResultObject<HIS_METY_METY> Create(HIS_METY_METY data)
        {
            ApiResultObject<HIS_METY_METY> result = new ApiResultObject<HIS_METY_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_METY_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMetyMetyCreate(param).Create(data);
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
        public ApiResultObject<HIS_METY_METY> Update(HIS_METY_METY data)
        {
            ApiResultObject<HIS_METY_METY> result = new ApiResultObject<HIS_METY_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_METY_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMetyMetyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_METY_METY> ChangeLock(long id)
        {
            ApiResultObject<HIS_METY_METY> result = new ApiResultObject<HIS_METY_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_METY_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMetyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_METY_METY> Lock(long id)
        {
            ApiResultObject<HIS_METY_METY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_METY_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMetyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_METY_METY> Unlock(long id)
        {
            ApiResultObject<HIS_METY_METY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_METY_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMetyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMetyMetyTruncate(param).Truncate(id);
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
        public ApiResultObject<List<HIS_METY_METY>> CreateList(List<HIS_METY_METY> listData)
        {
            ApiResultObject<List<HIS_METY_METY>> result = new ApiResultObject<List<HIS_METY_METY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_METY_METY> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMetyCreate(param).CreateList(listData);
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
        public ApiResultObject<List<HIS_METY_METY>> UpdateList(List<HIS_METY_METY> listData)
        {
            ApiResultObject<List<HIS_METY_METY>> result = new ApiResultObject<List<HIS_METY_METY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_METY_METY> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMetyUpdate(param).UpdateList(listData);
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
                    resultData = new HisMetyMetyTruncate(param).TruncateList(ids);
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
