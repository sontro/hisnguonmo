using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskAccess
{
    public partial class HisKskAccessManager : BusinessBase
    {
        public HisKskAccessManager()
            : base()
        {

        }
        
        public HisKskAccessManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_KSK_ACCESS>> Get(HisKskAccessFilterQuery filter)
        {
            ApiResultObject<List<HIS_KSK_ACCESS>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_KSK_ACCESS> resultData = null;
                if (valid)
                {
                    resultData = new HisKskAccessGet(param).Get(filter);
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
        public ApiResultObject<HIS_KSK_ACCESS> Create(HIS_KSK_ACCESS data)
        {
            ApiResultObject<HIS_KSK_ACCESS> result = new ApiResultObject<HIS_KSK_ACCESS>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_ACCESS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskAccessCreate(param).Create(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_KSK_ACCESS> Update(HIS_KSK_ACCESS data)
        {
            ApiResultObject<HIS_KSK_ACCESS> result = new ApiResultObject<HIS_KSK_ACCESS>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_ACCESS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskAccessUpdate(param).Update(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_KSK_ACCESS> ChangeLock(long id)
        {
            ApiResultObject<HIS_KSK_ACCESS> result = new ApiResultObject<HIS_KSK_ACCESS>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_ACCESS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskAccessLock(param).ChangeLock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_KSK_ACCESS> Lock(long id)
        {
            ApiResultObject<HIS_KSK_ACCESS> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_ACCESS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskAccessLock(param).Lock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_KSK_ACCESS> Unlock(long id)
        {
            ApiResultObject<HIS_KSK_ACCESS> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_ACCESS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskAccessLock(param).Unlock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
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
                    resultData = new HisKskAccessTruncate(param).Truncate(id);
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
        public ApiResultObject<KskAccessResultSDO> AssignEmployee(KskAccessSDO data)
        {
            ApiResultObject<KskAccessResultSDO> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                KskAccessResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskAccessAssignEmployee(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
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
