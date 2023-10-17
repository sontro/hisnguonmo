using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigenMety
{
    public partial class HisAntigenMetyManager : BusinessBase
    {
        public HisAntigenMetyManager()
            : base()
        {

        }
        
        public HisAntigenMetyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ANTIGEN_METY>> Get(HisAntigenMetyFilterQuery filter)
        {
            ApiResultObject<List<HIS_ANTIGEN_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ANTIGEN_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisAntigenMetyGet(param).Get(filter);
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
        public ApiResultObject<HIS_ANTIGEN_METY> Create(HIS_ANTIGEN_METY data)
        {
            ApiResultObject<HIS_ANTIGEN_METY> result = new ApiResultObject<HIS_ANTIGEN_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIGEN_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAntigenMetyCreate(param).Create(data);
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
        public ApiResultObject<HIS_ANTIGEN_METY> Update(HIS_ANTIGEN_METY data)
        {
            ApiResultObject<HIS_ANTIGEN_METY> result = new ApiResultObject<HIS_ANTIGEN_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIGEN_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAntigenMetyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ANTIGEN_METY> ChangeLock(long id)
        {
            ApiResultObject<HIS_ANTIGEN_METY> result = new ApiResultObject<HIS_ANTIGEN_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIGEN_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntigenMetyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTIGEN_METY> Lock(long id)
        {
            ApiResultObject<HIS_ANTIGEN_METY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIGEN_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntigenMetyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTIGEN_METY> Unlock(long id)
        {
            ApiResultObject<HIS_ANTIGEN_METY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIGEN_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntigenMetyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAntigenMetyTruncate(param).Truncate(id);
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
