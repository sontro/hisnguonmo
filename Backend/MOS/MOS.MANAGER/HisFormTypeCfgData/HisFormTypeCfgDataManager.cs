using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFormTypeCfgData
{
    public partial class HisFormTypeCfgDataManager : BusinessBase
    {
        public HisFormTypeCfgDataManager()
            : base()
        {

        }
        
        public HisFormTypeCfgDataManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_FORM_TYPE_CFG_DATA>> Get(HisFormTypeCfgDataFilterQuery filter)
        {
            ApiResultObject<List<HIS_FORM_TYPE_CFG_DATA>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_FORM_TYPE_CFG_DATA> resultData = null;
                if (valid)
                {
                    resultData = new HisFormTypeCfgDataGet(param).Get(filter);
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
        public ApiResultObject<HIS_FORM_TYPE_CFG_DATA> Create(HIS_FORM_TYPE_CFG_DATA data)
        {
            ApiResultObject<HIS_FORM_TYPE_CFG_DATA> result = new ApiResultObject<HIS_FORM_TYPE_CFG_DATA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_FORM_TYPE_CFG_DATA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisFormTypeCfgDataCreate(param).Create(data);
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
        public ApiResultObject<HIS_FORM_TYPE_CFG_DATA> Update(HIS_FORM_TYPE_CFG_DATA data)
        {
            ApiResultObject<HIS_FORM_TYPE_CFG_DATA> result = new ApiResultObject<HIS_FORM_TYPE_CFG_DATA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_FORM_TYPE_CFG_DATA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisFormTypeCfgDataUpdate(param).Update(data);
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
        public ApiResultObject<HIS_FORM_TYPE_CFG_DATA> ChangeLock(long id)
        {
            ApiResultObject<HIS_FORM_TYPE_CFG_DATA> result = new ApiResultObject<HIS_FORM_TYPE_CFG_DATA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FORM_TYPE_CFG_DATA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFormTypeCfgDataLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_FORM_TYPE_CFG_DATA> Lock(long id)
        {
            ApiResultObject<HIS_FORM_TYPE_CFG_DATA> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FORM_TYPE_CFG_DATA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFormTypeCfgDataLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_FORM_TYPE_CFG_DATA> Unlock(long id)
        {
            ApiResultObject<HIS_FORM_TYPE_CFG_DATA> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FORM_TYPE_CFG_DATA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFormTypeCfgDataLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisFormTypeCfgDataTruncate(param).Truncate(id);
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
