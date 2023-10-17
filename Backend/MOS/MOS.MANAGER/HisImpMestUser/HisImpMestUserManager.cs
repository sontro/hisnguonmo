using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisImpMestUser
{
    public partial class HisImpMestUserManager : BusinessBase
    {
        public HisImpMestUserManager()
            : base()
        {

        }
        
        public HisImpMestUserManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_IMP_MEST_USER>> Get(HisImpMestUserFilterQuery filter)
        {
            ApiResultObject<List<HIS_IMP_MEST_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_IMP_MEST_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestUserGet(param).Get(filter);
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
        public ApiResultObject<HIS_IMP_MEST_USER> Create(HIS_IMP_MEST_USER data)
        {
            ApiResultObject<HIS_IMP_MEST_USER> result = new ApiResultObject<HIS_IMP_MEST_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_USER resultData = null;
                if (valid && new HisImpMestUserCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_IMP_MEST_USER>> CreateOrReplace(HisImpMestUserSDO data)
        {
            ApiResultObject<List<HIS_IMP_MEST_USER>> result = new ApiResultObject<List<HIS_IMP_MEST_USER>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_IMP_MEST_USER> resultData = null;
                if (valid)
                {
                    new HisImpMestUserCreate(param).CreateOrReplace(data, ref resultData);
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
        public ApiResultObject<HIS_IMP_MEST_USER> Update(HIS_IMP_MEST_USER data)
        {
            ApiResultObject<HIS_IMP_MEST_USER> result = new ApiResultObject<HIS_IMP_MEST_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_USER resultData = null;
                if (valid && new HisImpMestUserUpdate(param).Update(data))
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
        public ApiResultObject<HIS_IMP_MEST_USER> ChangeLock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_USER> result = new ApiResultObject<HIS_IMP_MEST_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_USER resultData = null;
                if (valid)
                {
                    new HisImpMestUserLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_IMP_MEST_USER> Lock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_USER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_USER resultData = null;
                if (valid)
                {
                    new HisImpMestUserLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_IMP_MEST_USER> Unlock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_USER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_USER resultData = null;
                if (valid)
                {
                    new HisImpMestUserLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisImpMestUserTruncate(param).Truncate(id);
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
