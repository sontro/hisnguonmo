using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestBlood
{
    public partial class HisImpMestBloodManager : BusinessBase
    {
        public HisImpMestBloodManager()
            : base()
        {

        }
        
        public HisImpMestBloodManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_IMP_MEST_BLOOD>> Get(HisImpMestBloodFilterQuery filter)
        {
            ApiResultObject<List<HIS_IMP_MEST_BLOOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_IMP_MEST_BLOOD> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestBloodGet(param).Get(filter);
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
        public ApiResultObject<HIS_IMP_MEST_BLOOD> Create(HIS_IMP_MEST_BLOOD data)
        {
            ApiResultObject<HIS_IMP_MEST_BLOOD> result = new ApiResultObject<HIS_IMP_MEST_BLOOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_BLOOD resultData = null;
                if (valid && new HisImpMestBloodCreate(param).Create(data))
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
        public ApiResultObject<HIS_IMP_MEST_BLOOD> Update(HIS_IMP_MEST_BLOOD data)
        {
            ApiResultObject<HIS_IMP_MEST_BLOOD> result = new ApiResultObject<HIS_IMP_MEST_BLOOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_BLOOD resultData = null;
                if (valid && new HisImpMestBloodUpdate(param).Update(data))
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
        public ApiResultObject<HIS_IMP_MEST_BLOOD> ChangeLock(HIS_IMP_MEST_BLOOD data)
        {
            ApiResultObject<HIS_IMP_MEST_BLOOD> result = new ApiResultObject<HIS_IMP_MEST_BLOOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_BLOOD resultData = null;
                if (valid && new HisImpMestBloodLock(param).ChangeLock(data))
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
        public ApiResultObject<HIS_IMP_MEST_BLOOD> Lock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_BLOOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_BLOOD resultData = null;
                if (valid)
                {
                    new HisImpMestBloodLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_IMP_MEST_BLOOD> Unlock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_BLOOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_BLOOD resultData = null;
                if (valid)
                {
                    new HisImpMestBloodLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisImpMestBloodTruncate(param).Truncate(id);
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
