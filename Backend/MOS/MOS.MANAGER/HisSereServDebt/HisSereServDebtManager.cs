using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServDebt
{
    public partial class HisSereServDebtManager : BusinessBase
    {
        public HisSereServDebtManager()
            : base()
        {

        }
        
        public HisSereServDebtManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERE_SERV_DEBT>> Get(HisSereServDebtFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERE_SERV_DEBT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERE_SERV_DEBT> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServDebtGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERE_SERV_DEBT> Create(HIS_SERE_SERV_DEBT data)
        {
            ApiResultObject<HIS_SERE_SERV_DEBT> result = new ApiResultObject<HIS_SERE_SERV_DEBT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_DEBT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSereServDebtCreate(param).Create(data);
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
        public ApiResultObject<HIS_SERE_SERV_DEBT> Update(HIS_SERE_SERV_DEBT data)
        {
            ApiResultObject<HIS_SERE_SERV_DEBT> result = new ApiResultObject<HIS_SERE_SERV_DEBT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_DEBT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSereServDebtUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SERE_SERV_DEBT> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_DEBT> result = new ApiResultObject<HIS_SERE_SERV_DEBT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_DEBT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServDebtLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV_DEBT> Lock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_DEBT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_DEBT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServDebtLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV_DEBT> Unlock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_DEBT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_DEBT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServDebtLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSereServDebtTruncate(param).Truncate(id);
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
