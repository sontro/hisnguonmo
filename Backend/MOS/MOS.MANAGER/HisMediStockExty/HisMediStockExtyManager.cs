using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockExty
{
    public partial class HisMediStockExtyManager : BusinessBase
    {
        public HisMediStockExtyManager()
            : base()
        {

        }
        
        public HisMediStockExtyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDI_STOCK_EXTY>> Get(HisMediStockExtyFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_EXTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDI_STOCK_EXTY> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockExtyGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDI_STOCK_EXTY> Create(HIS_MEDI_STOCK_EXTY data)
        {
            ApiResultObject<HIS_MEDI_STOCK_EXTY> result = new ApiResultObject<HIS_MEDI_STOCK_EXTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_EXTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMediStockExtyCreate(param).Create(data);
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
        public ApiResultObject<HIS_MEDI_STOCK_EXTY> Update(HIS_MEDI_STOCK_EXTY data)
        {
            ApiResultObject<HIS_MEDI_STOCK_EXTY> result = new ApiResultObject<HIS_MEDI_STOCK_EXTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_EXTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMediStockExtyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MEDI_STOCK_EXTY> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDI_STOCK_EXTY> result = new ApiResultObject<HIS_MEDI_STOCK_EXTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_STOCK_EXTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediStockExtyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_STOCK_EXTY> Lock(long id)
        {
            ApiResultObject<HIS_MEDI_STOCK_EXTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_STOCK_EXTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediStockExtyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_STOCK_EXTY> Unlock(long id)
        {
            ApiResultObject<HIS_MEDI_STOCK_EXTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_STOCK_EXTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediStockExtyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMediStockExtyTruncate(param).Truncate(id);
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
