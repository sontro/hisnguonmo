using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    public partial class HisCarerCardBorrowManager : BusinessBase
    {
        public HisCarerCardBorrowManager()
            : base()
        {

        }
        
        public HisCarerCardBorrowManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_CARER_CARD_BORROW>> Get(HisCarerCardBorrowFilterQuery filter)
        {
            ApiResultObject<List<HIS_CARER_CARD_BORROW>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CARER_CARD_BORROW> resultData = null;
                if (valid)
                {
                    resultData = new HisCarerCardBorrowGet(param).Get(filter);
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
        public ApiResultObject<HIS_CARER_CARD_BORROW> Create(HIS_CARER_CARD_BORROW data)
        {
            ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARER_CARD_BORROW resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCarerCardBorrowCreate(param).Create(data);
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
        public ApiResultObject<HIS_CARER_CARD_BORROW> Update(HIS_CARER_CARD_BORROW data)
        {
            ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARER_CARD_BORROW resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCarerCardBorrowUpdate(param).Update(data);
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
        public ApiResultObject<HIS_CARER_CARD_BORROW> ChangeLock(long id)
        {
            ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARER_CARD_BORROW resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCarerCardBorrowLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_CARER_CARD_BORROW> Lock(long id)
        {
            ApiResultObject<HIS_CARER_CARD_BORROW> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARER_CARD_BORROW resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCarerCardBorrowLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CARER_CARD_BORROW> Unlock(long id)
        {
            ApiResultObject<HIS_CARER_CARD_BORROW> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARER_CARD_BORROW resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCarerCardBorrowLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisCarerCardBorrowTruncate(param).Truncate(id);
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
        public ApiResultObject<bool> Borrow(HisCarerCardBorrowSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisCarerCardBorrowSDOCreateList(param).Run(data);
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
        public ApiResultObject<HIS_CARER_CARD_BORROW> Lost(HisCarerCardBorrowLostSDO data)
        {
            ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARER_CARD_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCarerCardBorrowLost(param).Lost(data, ref resultData);
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
        public ApiResultObject<HIS_CARER_CARD_BORROW> UnLost(HisCarerCardBorrowUnLostSDO data)
        {
            ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARER_CARD_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCarerCardBorrowUnLost(param).UnLost(data, ref resultData);
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
        public ApiResultObject<HIS_CARER_CARD_BORROW> GiveBack(HisCarerCardBorrowGiveBackSDO data)
        {
            ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARER_CARD_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCarerCardBorrowGiveBack(param).GiveBack(data, ref resultData);
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
        public ApiResultObject<HIS_CARER_CARD_BORROW> UnGiveBack(HisCarerCardBorrowUnGiveBackSDO data)
        {
            ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARER_CARD_BORROW resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCarerCardBorrowUnGiveBack(param).UnGiveBack(data, ref resultData);
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
        public ApiResultObject<bool> DeleteSDO(HisCarerCardBorrowDeleteSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisCarerCardBorrowDeleteSDOProcessor(param).DeleteSDO(data);
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
