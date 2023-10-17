using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSubclinicalRsAdd
{
    public partial class HisSubclinicalRsAddManager : BusinessBase
    {
        public HisSubclinicalRsAddManager()
            : base()
        {

        }
        
        public HisSubclinicalRsAddManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SUBCLINICAL_RS_ADD>> Get(HisSubclinicalRsAddFilterQuery filter)
        {
            ApiResultObject<List<HIS_SUBCLINICAL_RS_ADD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SUBCLINICAL_RS_ADD> resultData = null;
                if (valid)
                {
                    resultData = new HisSubclinicalRsAddGet(param).Get(filter);
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
        public ApiResultObject<HIS_SUBCLINICAL_RS_ADD> Create(HIS_SUBCLINICAL_RS_ADD data)
        {
            ApiResultObject<HIS_SUBCLINICAL_RS_ADD> result = new ApiResultObject<HIS_SUBCLINICAL_RS_ADD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SUBCLINICAL_RS_ADD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSubclinicalRsAddCreate(param).Create(data);
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
        public ApiResultObject<HIS_SUBCLINICAL_RS_ADD> Update(HIS_SUBCLINICAL_RS_ADD data)
        {
            ApiResultObject<HIS_SUBCLINICAL_RS_ADD> result = new ApiResultObject<HIS_SUBCLINICAL_RS_ADD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SUBCLINICAL_RS_ADD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSubclinicalRsAddUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SUBCLINICAL_RS_ADD> ChangeLock(long id)
        {
            ApiResultObject<HIS_SUBCLINICAL_RS_ADD> result = new ApiResultObject<HIS_SUBCLINICAL_RS_ADD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SUBCLINICAL_RS_ADD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSubclinicalRsAddLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SUBCLINICAL_RS_ADD> Lock(long id)
        {
            ApiResultObject<HIS_SUBCLINICAL_RS_ADD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SUBCLINICAL_RS_ADD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSubclinicalRsAddLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SUBCLINICAL_RS_ADD> Unlock(long id)
        {
            ApiResultObject<HIS_SUBCLINICAL_RS_ADD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SUBCLINICAL_RS_ADD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSubclinicalRsAddLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSubclinicalRsAddTruncate(param).Truncate(id);
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
