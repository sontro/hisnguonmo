using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.Filter;
using MOS.SDO;

namespace MOS.MANAGER.HisNumOrderBlock
{
    public partial class HisNumOrderBlockManager : BusinessBase
    {
        public HisNumOrderBlockManager()
            : base()
        {

        }
        
        public HisNumOrderBlockManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_NUM_ORDER_BLOCK>> Get(HisNumOrderBlockFilterQuery filter)
        {
            ApiResultObject<List<HIS_NUM_ORDER_BLOCK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_NUM_ORDER_BLOCK> resultData = null;
                if (valid)
                {
                    resultData = new HisNumOrderBlockGet(param).Get(filter);
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
        public ApiResultObject<List<HisNumOrderBlockSDO>> GetOccupiedStatus(HisNumOrderBlockOccupiedStatusFilter filter)
        {
            ApiResultObject<List<HisNumOrderBlockSDO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisNumOrderBlockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisNumOrderBlockGet(param).GetOccupiedStatus(filter);
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
        public ApiResultObject<HIS_NUM_ORDER_BLOCK> Create(HIS_NUM_ORDER_BLOCK data)
        {
            ApiResultObject<HIS_NUM_ORDER_BLOCK> result = new ApiResultObject<HIS_NUM_ORDER_BLOCK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_NUM_ORDER_BLOCK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisNumOrderBlockCreate(param).Create(data);
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
        public ApiResultObject<List<HIS_NUM_ORDER_BLOCK>> CreateList(List<HIS_NUM_ORDER_BLOCK> data)
        {
            ApiResultObject<List<HIS_NUM_ORDER_BLOCK>> result = new ApiResultObject<List<HIS_NUM_ORDER_BLOCK>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_NUM_ORDER_BLOCK> resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisNumOrderBlockCreate(param).CreateList(data);
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
        public ApiResultObject<HIS_NUM_ORDER_BLOCK> Update(HIS_NUM_ORDER_BLOCK data)
        {
            ApiResultObject<HIS_NUM_ORDER_BLOCK> result = new ApiResultObject<HIS_NUM_ORDER_BLOCK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_NUM_ORDER_BLOCK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisNumOrderBlockUpdate(param).Update(data);
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
        public ApiResultObject<HIS_NUM_ORDER_BLOCK> ChangeLock(long id)
        {
            ApiResultObject<HIS_NUM_ORDER_BLOCK> result = new ApiResultObject<HIS_NUM_ORDER_BLOCK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_NUM_ORDER_BLOCK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisNumOrderBlockLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_NUM_ORDER_BLOCK> Lock(long id)
        {
            ApiResultObject<HIS_NUM_ORDER_BLOCK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_NUM_ORDER_BLOCK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisNumOrderBlockLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_NUM_ORDER_BLOCK> Unlock(long id)
        {
            ApiResultObject<HIS_NUM_ORDER_BLOCK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_NUM_ORDER_BLOCK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisNumOrderBlockLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisNumOrderBlockTruncate(param).Truncate(id);
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
