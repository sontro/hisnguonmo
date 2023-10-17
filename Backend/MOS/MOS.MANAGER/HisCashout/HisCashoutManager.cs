using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisCashout
{
    public partial class HisCashoutManager : BusinessBase
    {
        public HisCashoutManager()
            : base()
        {

        }
        
        public HisCashoutManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_CASHOUT>> Get(HisCashoutFilterQuery filter)
        {
            ApiResultObject<List<HIS_CASHOUT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CASHOUT> resultData = null;
                if (valid)
                {
                    resultData = new HisCashoutGet(param).Get(filter);
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
        public ApiResultObject<HIS_CASHOUT> Create(HisCashoutSDO data)
        {
            ApiResultObject<HIS_CASHOUT> result = new ApiResultObject<HIS_CASHOUT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CASHOUT resultData = null;
                if (valid)
                {
                    new HisCashoutCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HIS_CASHOUT> Update(HisCashoutSDO data)
        {
            ApiResultObject<HIS_CASHOUT> result = new ApiResultObject<HIS_CASHOUT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CASHOUT resultData = null;
                if (valid)
                {
                    new HisCashoutUpdate(param).Update(data, ref resultData);
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
        public ApiResultObject<HIS_CASHOUT> ChangeLock(long id)
        {
            ApiResultObject<HIS_CASHOUT> result = new ApiResultObject<HIS_CASHOUT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CASHOUT resultData = null;
                if (valid)
                {
                    new HisCashoutLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_CASHOUT> Lock(long id)
        {
            ApiResultObject<HIS_CASHOUT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CASHOUT resultData = null;
                if (valid)
                {
                    new HisCashoutLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CASHOUT> Unlock(long id)
        {
            ApiResultObject<HIS_CASHOUT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CASHOUT resultData = null;
                if (valid)
                {
                    new HisCashoutLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisCashoutTruncate(param).Truncate(id);
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
