using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisObeyContraindi
{
    public partial class HisObeyContraindiManager : BusinessBase
    {
        public HisObeyContraindiManager()
            : base()
        {

        }
        
        public HisObeyContraindiManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_OBEY_CONTRAINDI>> Get(HisObeyContraindiFilterQuery filter)
        {
            ApiResultObject<List<HIS_OBEY_CONTRAINDI>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_OBEY_CONTRAINDI> resultData = null;
                if (valid)
                {
                    resultData = new HisObeyContraindiGet(param).Get(filter);
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
        public ApiResultObject<HIS_OBEY_CONTRAINDI> Create(HIS_OBEY_CONTRAINDI data)
        {
            ApiResultObject<HIS_OBEY_CONTRAINDI> result = new ApiResultObject<HIS_OBEY_CONTRAINDI>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_OBEY_CONTRAINDI resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisObeyContraindiCreate(param).Create(data);
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
        public ApiResultObject<HIS_OBEY_CONTRAINDI> Update(HIS_OBEY_CONTRAINDI data)
        {
            ApiResultObject<HIS_OBEY_CONTRAINDI> result = new ApiResultObject<HIS_OBEY_CONTRAINDI>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_OBEY_CONTRAINDI resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisObeyContraindiUpdate(param).Update(data);
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
        public ApiResultObject<List<HIS_OBEY_CONTRAINDI>> UpdateList(List<HIS_OBEY_CONTRAINDI> listData)
        {
            ApiResultObject<List<HIS_OBEY_CONTRAINDI>> result = new ApiResultObject<List<HIS_OBEY_CONTRAINDI>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_OBEY_CONTRAINDI> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisObeyContraindiUpdate(param).UpdateList(listData);
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
        public ApiResultObject<HIS_OBEY_CONTRAINDI> ChangeLock(long id)
        {
            ApiResultObject<HIS_OBEY_CONTRAINDI> result = new ApiResultObject<HIS_OBEY_CONTRAINDI>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_OBEY_CONTRAINDI resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisObeyContraindiLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_OBEY_CONTRAINDI> Lock(long id)
        {
            ApiResultObject<HIS_OBEY_CONTRAINDI> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_OBEY_CONTRAINDI resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisObeyContraindiLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_OBEY_CONTRAINDI> Unlock(long id)
        {
            ApiResultObject<HIS_OBEY_CONTRAINDI> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_OBEY_CONTRAINDI resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisObeyContraindiLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisObeyContraindiTruncate(param).Truncate(id);
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
