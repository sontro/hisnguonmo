using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteRole
{
    public partial class HisExecuteRoleManager : BusinessBase
    {
        public HisExecuteRoleManager()
            : base()
        {

        }
        
        public HisExecuteRoleManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EXECUTE_ROLE>> Get(HisExecuteRoleFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXECUTE_ROLE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXECUTE_ROLE> resultData = null;
                if (valid)
                {
                    resultData = new HisExecuteRoleGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXECUTE_ROLE> Create(HIS_EXECUTE_ROLE data)
        {
            ApiResultObject<HIS_EXECUTE_ROLE> result = new ApiResultObject<HIS_EXECUTE_ROLE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXECUTE_ROLE resultData = null;
                if (valid && new HisExecuteRoleCreate(param).Create(data))
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
        public ApiResultObject<HIS_EXECUTE_ROLE> Update(HIS_EXECUTE_ROLE data)
        {
            ApiResultObject<HIS_EXECUTE_ROLE> result = new ApiResultObject<HIS_EXECUTE_ROLE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXECUTE_ROLE resultData = null;
                if (valid && new HisExecuteRoleUpdate(param).Update(data))
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
        public ApiResultObject<HIS_EXECUTE_ROLE> ChangeLock(HIS_EXECUTE_ROLE data)
        {
            ApiResultObject<HIS_EXECUTE_ROLE> result = new ApiResultObject<HIS_EXECUTE_ROLE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXECUTE_ROLE resultData = null;
                if (valid && new HisExecuteRoleLock(param).ChangeLock(data))
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
        public ApiResultObject<HIS_EXECUTE_ROLE> Lock(long id)
        {
            ApiResultObject<HIS_EXECUTE_ROLE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXECUTE_ROLE resultData = null;
                if (valid)
                {
                    new HisExecuteRoleLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EXECUTE_ROLE> Unlock(long id)
        {
            ApiResultObject<HIS_EXECUTE_ROLE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXECUTE_ROLE resultData = null;
                if (valid)
                {
                    new HisExecuteRoleLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisExecuteRoleTruncate(param).Truncate(id);
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
