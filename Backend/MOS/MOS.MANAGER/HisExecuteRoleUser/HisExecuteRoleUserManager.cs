using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisExecuteRoleUser
{
    public partial class HisExecuteRoleUserManager : BusinessBase
    {
        public HisExecuteRoleUserManager()
            : base()
        {

        }

        public HisExecuteRoleUserManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_EXECUTE_ROLE_USER>> Get(HisExecuteRoleUserFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXECUTE_ROLE_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXECUTE_ROLE_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisExecuteRoleUserGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXECUTE_ROLE_USER> Create(HIS_EXECUTE_ROLE_USER data)
        {
            ApiResultObject<HIS_EXECUTE_ROLE_USER> result = new ApiResultObject<HIS_EXECUTE_ROLE_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXECUTE_ROLE_USER resultData = null;
                if (valid && new HisExecuteRoleUserCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_EXECUTE_ROLE_USER>> CreateList(List<HIS_EXECUTE_ROLE_USER> data)
        {
            ApiResultObject<List<HIS_EXECUTE_ROLE_USER>> result = new ApiResultObject<List<HIS_EXECUTE_ROLE_USER>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_EXECUTE_ROLE_USER> resultData = null;
                if (valid && new HisExecuteRoleUserCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_EXECUTE_ROLE_USER> Update(HIS_EXECUTE_ROLE_USER data)
        {
            ApiResultObject<HIS_EXECUTE_ROLE_USER> result = new ApiResultObject<HIS_EXECUTE_ROLE_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXECUTE_ROLE_USER resultData = null;
                if (valid && new HisExecuteRoleUserUpdate(param).Update(data))
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
        public ApiResultObject<HIS_EXECUTE_ROLE_USER> ChangeLock(long id)
        {
            ApiResultObject<HIS_EXECUTE_ROLE_USER> result = new ApiResultObject<HIS_EXECUTE_ROLE_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXECUTE_ROLE_USER resultData = null;
                if (valid)
                {
                    new HisExecuteRoleUserLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EXECUTE_ROLE_USER> Lock(long id)
        {
            ApiResultObject<HIS_EXECUTE_ROLE_USER> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXECUTE_ROLE_USER resultData = null;
                if (valid)
                {
                    new HisExecuteRoleUserLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EXECUTE_ROLE_USER> Unlock(long id)
        {
            ApiResultObject<HIS_EXECUTE_ROLE_USER> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXECUTE_ROLE_USER resultData = null;
                if (valid)
                {
                    new HisExecuteRoleUserLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisExecuteRoleUserTruncate(param).Truncate(id);
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
                    resultData = new HisExecuteRoleUserTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_EXECUTE_ROLE_USER>> CopyByRole(HisExecuteRoleUserCopyByRoleSDO data)
        {
            ApiResultObject<List<HIS_EXECUTE_ROLE_USER>> result = new ApiResultObject<List<HIS_EXECUTE_ROLE_USER>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_EXECUTE_ROLE_USER> resultData = null;
                if (valid)
                {
                    new HisExecuteRoleUserCopyByRole(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_EXECUTE_ROLE_USER>> CopyByLoginname(HisExecuteRoleUserCopyByLoginnameSDO data)
        {
            ApiResultObject<List<HIS_EXECUTE_ROLE_USER>> result = new ApiResultObject<List<HIS_EXECUTE_ROLE_USER>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_EXECUTE_ROLE_USER> resultData = null;
                if (valid)
                {
                    new HisExecuteRoleUserCopyByLoginname(param).Run(data, ref resultData);
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
