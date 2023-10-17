using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.TDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmployee
{
    public partial class HisEmployeeManager : BusinessBase
    {
        public HisEmployeeManager()
            : base()
        {

        }
        
        public HisEmployeeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EMPLOYEE>> Get(HisEmployeeFilterQuery filter)
        {
            ApiResultObject<List<HIS_EMPLOYEE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EMPLOYEE> resultData = null;
                if (valid)
                {
                    resultData = new HisEmployeeGet(param).Get(filter);
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
        public ApiResultObject<List<GetUserDetailForEmrTDO>> GetUserDetailForEmr()
        {
            ApiResultObject<List<GetUserDetailForEmrTDO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<GetUserDetailForEmrTDO> resultData = null;
                if (valid)
                {
                    resultData = new HisEmployeeGet(param).GetUserDetailForEmr();
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
        public ApiResultObject<HIS_EMPLOYEE> Create(HIS_EMPLOYEE data)
        {
            ApiResultObject<HIS_EMPLOYEE> result = new ApiResultObject<HIS_EMPLOYEE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMPLOYEE resultData = null;
                if (valid && new HisEmployeeCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_EMPLOYEE>> CreateList(List<HIS_EMPLOYEE> data)
        {
            ApiResultObject<List<HIS_EMPLOYEE>> result = new ApiResultObject<List<HIS_EMPLOYEE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_EMPLOYEE> resultData = null;
                if (valid && new HisEmployeeCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_EMPLOYEE> Update(HIS_EMPLOYEE data)
        {
            ApiResultObject<HIS_EMPLOYEE> result = new ApiResultObject<HIS_EMPLOYEE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMPLOYEE resultData = null;
                if (valid && new HisEmployeeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_EMPLOYEE> ChangeLock(long id)
        {
            ApiResultObject<HIS_EMPLOYEE> result = new ApiResultObject<HIS_EMPLOYEE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMPLOYEE resultData = null;
                if (valid)
                {
                    new HisEmployeeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EMPLOYEE> Lock(long id)
        {
            ApiResultObject<HIS_EMPLOYEE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMPLOYEE resultData = null;
                if (valid)
                {
                    new HisEmployeeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EMPLOYEE> Unlock(long id)
        {
            ApiResultObject<HIS_EMPLOYEE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMPLOYEE resultData = null;
                if (valid)
                {
                    new HisEmployeeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisEmployeeTruncate(param).Truncate(id);
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
                    resultData = new HisEmployeeTruncate(param).TruncateList(ids);
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
