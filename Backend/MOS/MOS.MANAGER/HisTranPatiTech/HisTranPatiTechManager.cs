using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiTech
{
    public partial class HisTranPatiTechManager : BusinessBase
    {
        public HisTranPatiTechManager()
            : base()
        {

        }
        
        public HisTranPatiTechManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TRAN_PATI_TECH>> Get(HisTranPatiTechFilterQuery filter)
        {
            ApiResultObject<List<HIS_TRAN_PATI_TECH>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TRAN_PATI_TECH> resultData = null;
                if (valid)
                {
                    resultData = new HisTranPatiTechGet(param).Get(filter);
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
        public ApiResultObject<HIS_TRAN_PATI_TECH> Create(HIS_TRAN_PATI_TECH data)
        {
            ApiResultObject<HIS_TRAN_PATI_TECH> result = new ApiResultObject<HIS_TRAN_PATI_TECH>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRAN_PATI_TECH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTranPatiTechCreate(param).Create(data);
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
        public ApiResultObject<HIS_TRAN_PATI_TECH> Update(HIS_TRAN_PATI_TECH data)
        {
            ApiResultObject<HIS_TRAN_PATI_TECH> result = new ApiResultObject<HIS_TRAN_PATI_TECH>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRAN_PATI_TECH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTranPatiTechUpdate(param).Update(data);
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
        public ApiResultObject<HIS_TRAN_PATI_TECH> ChangeLock(long id)
        {
            ApiResultObject<HIS_TRAN_PATI_TECH> result = new ApiResultObject<HIS_TRAN_PATI_TECH>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRAN_PATI_TECH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTranPatiTechLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_TRAN_PATI_TECH> Lock(long id)
        {
            ApiResultObject<HIS_TRAN_PATI_TECH> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRAN_PATI_TECH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTranPatiTechLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_TRAN_PATI_TECH> Unlock(long id)
        {
            ApiResultObject<HIS_TRAN_PATI_TECH> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRAN_PATI_TECH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTranPatiTechLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisTranPatiTechTruncate(param).Truncate(id);
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
