using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBillGoods
{
    public partial class HisBillGoodsManager : BusinessBase
    {
        public HisBillGoodsManager()
            : base()
        {

        }
        
        public HisBillGoodsManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BILL_GOODS>> Get(HisBillGoodsFilterQuery filter)
        {
            ApiResultObject<List<HIS_BILL_GOODS>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BILL_GOODS> resultData = null;
                if (valid)
                {
                    resultData = new HisBillGoodsGet(param).Get(filter);
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
        public ApiResultObject<HIS_BILL_GOODS> Create(HIS_BILL_GOODS data)
        {
            ApiResultObject<HIS_BILL_GOODS> result = new ApiResultObject<HIS_BILL_GOODS>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BILL_GOODS resultData = null;
                if (valid && new HisBillGoodsCreate(param).Create(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_BILL_GOODS> Update(HIS_BILL_GOODS data)
        {
            ApiResultObject<HIS_BILL_GOODS> result = new ApiResultObject<HIS_BILL_GOODS>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BILL_GOODS resultData = null;
                if (valid && new HisBillGoodsUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_BILL_GOODS> ChangeLock(long id)
        {
            ApiResultObject<HIS_BILL_GOODS> result = new ApiResultObject<HIS_BILL_GOODS>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BILL_GOODS resultData = null;
                if (valid)
                {
                    new HisBillGoodsLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BILL_GOODS> Lock(long id)
        {
            ApiResultObject<HIS_BILL_GOODS> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BILL_GOODS resultData = null;
                if (valid)
                {
                    new HisBillGoodsLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BILL_GOODS> Unlock(long id)
        {
            ApiResultObject<HIS_BILL_GOODS> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BILL_GOODS resultData = null;
                if (valid)
                {
                    new HisBillGoodsLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBillGoodsTruncate(param).Truncate(id);
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
