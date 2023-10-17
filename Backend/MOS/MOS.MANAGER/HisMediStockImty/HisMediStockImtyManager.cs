using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockImty
{
    public partial class HisMediStockImtyManager : BusinessBase
    {
        public HisMediStockImtyManager()
            : base()
        {

        }
        
        public HisMediStockImtyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDI_STOCK_IMTY>> Get(HisMediStockImtyFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_IMTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDI_STOCK_IMTY> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockImtyGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDI_STOCK_IMTY> Create(HIS_MEDI_STOCK_IMTY data)
        {
            ApiResultObject<HIS_MEDI_STOCK_IMTY> result = new ApiResultObject<HIS_MEDI_STOCK_IMTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_IMTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMediStockImtyCreate(param).Create(data);
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
        public ApiResultObject<HIS_MEDI_STOCK_IMTY> Update(HIS_MEDI_STOCK_IMTY data)
        {
            ApiResultObject<HIS_MEDI_STOCK_IMTY> result = new ApiResultObject<HIS_MEDI_STOCK_IMTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_IMTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMediStockImtyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MEDI_STOCK_IMTY> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDI_STOCK_IMTY> result = new ApiResultObject<HIS_MEDI_STOCK_IMTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_STOCK_IMTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediStockImtyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_STOCK_IMTY> Lock(long id)
        {
            ApiResultObject<HIS_MEDI_STOCK_IMTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_STOCK_IMTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediStockImtyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_STOCK_IMTY> Unlock(long id)
        {
            ApiResultObject<HIS_MEDI_STOCK_IMTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_STOCK_IMTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediStockImtyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMediStockImtyTruncate(param).Truncate(id);
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
