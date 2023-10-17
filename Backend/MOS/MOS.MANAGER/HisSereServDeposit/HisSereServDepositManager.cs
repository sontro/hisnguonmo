using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServDeposit
{
    public partial class HisSereServDepositManager : BusinessBase
    {
        public HisSereServDepositManager()
            : base()
        {

        }
        
        public HisSereServDepositManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERE_SERV_DEPOSIT>> Get(HisSereServDepositFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERE_SERV_DEPOSIT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERE_SERV_DEPOSIT> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServDepositGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERE_SERV_DEPOSIT> Create(HIS_SERE_SERV_DEPOSIT data)
        {
            ApiResultObject<HIS_SERE_SERV_DEPOSIT> result = new ApiResultObject<HIS_SERE_SERV_DEPOSIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_DEPOSIT resultData = null;
                if (valid && new HisSereServDepositCreate(param).Create(data))
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
        public ApiResultObject<HIS_SERE_SERV_DEPOSIT> Update(HIS_SERE_SERV_DEPOSIT data)
        {
            ApiResultObject<HIS_SERE_SERV_DEPOSIT> result = new ApiResultObject<HIS_SERE_SERV_DEPOSIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_DEPOSIT resultData = null;
                if (valid && new HisSereServDepositUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SERE_SERV_DEPOSIT> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_DEPOSIT> result = new ApiResultObject<HIS_SERE_SERV_DEPOSIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_DEPOSIT resultData = null;
                if (valid)
                {
                    new HisSereServDepositLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV_DEPOSIT> Lock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_DEPOSIT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_DEPOSIT resultData = null;
                if (valid)
                {
                    new HisSereServDepositLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV_DEPOSIT> Unlock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_DEPOSIT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_DEPOSIT resultData = null;
                if (valid)
                {
                    new HisSereServDepositLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSereServDepositTruncate(param).Truncate(id);
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
