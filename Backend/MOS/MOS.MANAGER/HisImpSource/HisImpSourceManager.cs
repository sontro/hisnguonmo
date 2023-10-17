using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpSource
{
    public partial class HisImpSourceManager : BusinessBase
    {
        public HisImpSourceManager()
            : base()
        {

        }
        
        public HisImpSourceManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_IMP_SOURCE>> Get(HisImpSourceFilterQuery filter)
        {
            ApiResultObject<List<HIS_IMP_SOURCE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_IMP_SOURCE> resultData = null;
                if (valid)
                {
                    resultData = new HisImpSourceGet(param).Get(filter);
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
        public ApiResultObject<HIS_IMP_SOURCE> Create(HIS_IMP_SOURCE data)
        {
            ApiResultObject<HIS_IMP_SOURCE> result = new ApiResultObject<HIS_IMP_SOURCE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_SOURCE resultData = null;
                if (valid && new HisImpSourceCreate(param).Create(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_IMP_SOURCE> Update(HIS_IMP_SOURCE data)
        {
            ApiResultObject<HIS_IMP_SOURCE> result = new ApiResultObject<HIS_IMP_SOURCE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_SOURCE resultData = null;
                if (valid && new HisImpSourceUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_IMP_SOURCE> ChangeLock(HIS_IMP_SOURCE data)
        {
            ApiResultObject<HIS_IMP_SOURCE> result = new ApiResultObject<HIS_IMP_SOURCE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_SOURCE resultData = null;
                if (valid && new HisImpSourceLock(param).ChangeLock(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_IMP_SOURCE> Lock(long id)
        {
            ApiResultObject<HIS_IMP_SOURCE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_SOURCE resultData = null;
                if (valid)
                {
                    new HisImpSourceLock(param).Lock(id, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_IMP_SOURCE> Unlock(long id)
        {
            ApiResultObject<HIS_IMP_SOURCE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_SOURCE resultData = null;
                if (valid)
                {
                    new HisImpSourceLock(param).Unlock(id, ref resultData);
                }
                result = this.PackSingleResult(resultData);
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
                    resultData = new HisImpSourceTruncate(param).Truncate(id);
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
