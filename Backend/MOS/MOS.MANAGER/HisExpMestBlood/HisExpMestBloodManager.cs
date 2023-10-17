using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestBlood
{
    public partial class HisExpMestBloodManager : BusinessBase
    {
        public HisExpMestBloodManager()
            : base()
        {

        }
        
        public HisExpMestBloodManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EXP_MEST_BLOOD>> Get(HisExpMestBloodFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXP_MEST_BLOOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXP_MEST_BLOOD> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestBloodGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXP_MEST_BLOOD> Create(HIS_EXP_MEST_BLOOD data)
        {
            ApiResultObject<HIS_EXP_MEST_BLOOD> result = new ApiResultObject<HIS_EXP_MEST_BLOOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_BLOOD resultData = null;
                if (valid && new HisExpMestBloodCreate(param).Create(data))
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
        public ApiResultObject<HIS_EXP_MEST_BLOOD> Update(HIS_EXP_MEST_BLOOD data)
        {
            ApiResultObject<HIS_EXP_MEST_BLOOD> result = new ApiResultObject<HIS_EXP_MEST_BLOOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_BLOOD resultData = null;
                if (valid && new HisExpMestBloodUpdate(param).Update(data))
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
        public ApiResultObject<HIS_EXP_MEST_BLOOD> ChangeLock(HIS_EXP_MEST_BLOOD data)
        {
            ApiResultObject<HIS_EXP_MEST_BLOOD> result = new ApiResultObject<HIS_EXP_MEST_BLOOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_BLOOD resultData = null;
                if (valid && new HisExpMestBloodLock(param).ChangeLock(data))
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
        public ApiResultObject<HIS_EXP_MEST_BLOOD> Lock(long id)
        {
            ApiResultObject<HIS_EXP_MEST_BLOOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_BLOOD resultData = null;
                if (valid)
                {
                    new HisExpMestBloodLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST_BLOOD> Unlock(long id)
        {
            ApiResultObject<HIS_EXP_MEST_BLOOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_BLOOD resultData = null;
                if (valid)
                {
                    new HisExpMestBloodLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisExpMestBloodTruncate(param).Truncate(id);
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
