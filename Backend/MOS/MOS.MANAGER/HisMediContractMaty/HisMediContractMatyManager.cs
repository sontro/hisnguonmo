using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediContractMaty
{
    public partial class HisMediContractMatyManager : BusinessBase
    {
        public HisMediContractMatyManager()
            : base()
        {

        }
        
        public HisMediContractMatyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDI_CONTRACT_MATY>> Get(HisMediContractMatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDI_CONTRACT_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDI_CONTRACT_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMediContractMatyGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDI_CONTRACT_MATY> Create(HIS_MEDI_CONTRACT_MATY data)
        {
            ApiResultObject<HIS_MEDI_CONTRACT_MATY> result = new ApiResultObject<HIS_MEDI_CONTRACT_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_CONTRACT_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMediContractMatyCreate(param).Create(data);
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
        public ApiResultObject<HIS_MEDI_CONTRACT_MATY> Update(HIS_MEDI_CONTRACT_MATY data)
        {
            ApiResultObject<HIS_MEDI_CONTRACT_MATY> result = new ApiResultObject<HIS_MEDI_CONTRACT_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_CONTRACT_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMediContractMatyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MEDI_CONTRACT_MATY> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDI_CONTRACT_MATY> result = new ApiResultObject<HIS_MEDI_CONTRACT_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_CONTRACT_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediContractMatyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_CONTRACT_MATY> Lock(long id)
        {
            ApiResultObject<HIS_MEDI_CONTRACT_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_CONTRACT_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediContractMatyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_CONTRACT_MATY> Unlock(long id)
        {
            ApiResultObject<HIS_MEDI_CONTRACT_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_CONTRACT_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediContractMatyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMediContractMatyTruncate(param).Truncate(id);
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
