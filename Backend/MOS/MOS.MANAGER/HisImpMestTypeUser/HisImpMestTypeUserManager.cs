using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisImpMestTypeUser
{
    public partial class HisImpMestTypeUserManager : BusinessBase
    {
        public HisImpMestTypeUserManager()
            : base()
        {

        }

        public HisImpMestTypeUserManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>> Get(HisImpMestTypeUserFilterQuery filter)
        {
            ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_IMP_MEST_TYPE_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestTypeUserGet(param).Get(filter);
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
        public ApiResultObject<HIS_IMP_MEST_TYPE_USER> Create(HIS_IMP_MEST_TYPE_USER data)
        {
            ApiResultObject<HIS_IMP_MEST_TYPE_USER> result = new ApiResultObject<HIS_IMP_MEST_TYPE_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_TYPE_USER resultData = null;
                if (valid && new HisImpMestTypeUserCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>> CreateList(List<HIS_IMP_MEST_TYPE_USER> data)
        {
            ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>> result = new ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_IMP_MEST_TYPE_USER> resultData = null;
                if (valid && new HisImpMestTypeUserCreate(param).CreateList(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_IMP_MEST_TYPE_USER> Update(HIS_IMP_MEST_TYPE_USER data)
        {
            ApiResultObject<HIS_IMP_MEST_TYPE_USER> result = new ApiResultObject<HIS_IMP_MEST_TYPE_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_TYPE_USER resultData = null;
                if (valid && new HisImpMestTypeUserUpdate(param).Update(data))
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
        public ApiResultObject<HIS_IMP_MEST_TYPE_USER> ChangeLock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_TYPE_USER> result = new ApiResultObject<HIS_IMP_MEST_TYPE_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_TYPE_USER resultData = null;
                if (valid)
                {
                    new HisImpMestTypeUserLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_IMP_MEST_TYPE_USER> Lock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_TYPE_USER> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_TYPE_USER resultData = null;
                if (valid)
                {
                    new HisImpMestTypeUserLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_IMP_MEST_TYPE_USER> Unlock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_TYPE_USER> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_TYPE_USER resultData = null;
                if (valid)
                {
                    new HisImpMestTypeUserLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisImpMestTypeUserTruncate(param).Truncate(id);
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
                    resultData = new HisImpMestTypeUserTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>> CopyByType(HisImpMestTypeUserCopyByTypeSDO data)
        {
            ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>> result = new ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_IMP_MEST_TYPE_USER> resultData = null;
                if (valid)
                {
                    new HisImpMestTypeUserCopyByType(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>> CopyByLoginname(HisImpMestTypeUserCopyByLoginnameSDO data)
        {
            ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>> result = new ApiResultObject<List<HIS_IMP_MEST_TYPE_USER>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_IMP_MEST_TYPE_USER> resultData = null;
                if (valid)
                {
                    new HisImpMestTypeUserCopyByLoginname(param).Run(data, ref resultData);
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
    }
}
