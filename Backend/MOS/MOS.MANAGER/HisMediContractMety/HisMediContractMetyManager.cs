using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediContractMety
{
    public partial class HisMediContractMetyManager : BusinessBase
    {
        public HisMediContractMetyManager()
            : base()
        {

        }
        
        public HisMediContractMetyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDI_CONTRACT_METY>> Get(HisMediContractMetyFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDI_CONTRACT_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDI_CONTRACT_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisMediContractMetyGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDI_CONTRACT_METY> Create(HIS_MEDI_CONTRACT_METY data)
        {
            ApiResultObject<HIS_MEDI_CONTRACT_METY> result = new ApiResultObject<HIS_MEDI_CONTRACT_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_CONTRACT_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMediContractMetyCreate(param).Create(data);
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
        public ApiResultObject<HIS_MEDI_CONTRACT_METY> Update(HIS_MEDI_CONTRACT_METY data)
        {
            ApiResultObject<HIS_MEDI_CONTRACT_METY> result = new ApiResultObject<HIS_MEDI_CONTRACT_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_CONTRACT_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMediContractMetyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MEDI_CONTRACT_METY> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDI_CONTRACT_METY> result = new ApiResultObject<HIS_MEDI_CONTRACT_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_CONTRACT_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediContractMetyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_CONTRACT_METY> Lock(long id)
        {
            ApiResultObject<HIS_MEDI_CONTRACT_METY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_CONTRACT_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediContractMetyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_CONTRACT_METY> Unlock(long id)
        {
            ApiResultObject<HIS_MEDI_CONTRACT_METY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_CONTRACT_METY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediContractMetyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMediContractMetyTruncate(param).Truncate(id);
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
