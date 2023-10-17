using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPaanLiquid
{
    public partial class HisPaanLiquidManager : BusinessBase
    {
        public HisPaanLiquidManager()
            : base()
        {

        }
        
        public HisPaanLiquidManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PAAN_LIQUID>> Get(HisPaanLiquidFilterQuery filter)
        {
            ApiResultObject<List<HIS_PAAN_LIQUID>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PAAN_LIQUID> resultData = null;
                if (valid)
                {
                    resultData = new HisPaanLiquidGet(param).Get(filter);
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
        public ApiResultObject<HIS_PAAN_LIQUID> Create(HIS_PAAN_LIQUID data)
        {
            ApiResultObject<HIS_PAAN_LIQUID> result = new ApiResultObject<HIS_PAAN_LIQUID>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PAAN_LIQUID resultData = null;
                if (valid && new HisPaanLiquidCreate(param).Create(data))
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
        public ApiResultObject<HIS_PAAN_LIQUID> Update(HIS_PAAN_LIQUID data)
        {
            ApiResultObject<HIS_PAAN_LIQUID> result = new ApiResultObject<HIS_PAAN_LIQUID>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PAAN_LIQUID resultData = null;
                if (valid && new HisPaanLiquidUpdate(param).Update(data))
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
        public ApiResultObject<HIS_PAAN_LIQUID> ChangeLock(long id)
        {
            ApiResultObject<HIS_PAAN_LIQUID> result = new ApiResultObject<HIS_PAAN_LIQUID>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PAAN_LIQUID resultData = null;
                if (valid)
                {
                    new HisPaanLiquidLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PAAN_LIQUID> Lock(long id)
        {
            ApiResultObject<HIS_PAAN_LIQUID> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PAAN_LIQUID resultData = null;
                if (valid)
                {
                    new HisPaanLiquidLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PAAN_LIQUID> Unlock(long id)
        {
            ApiResultObject<HIS_PAAN_LIQUID> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PAAN_LIQUID resultData = null;
                if (valid)
                {
                    new HisPaanLiquidLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPaanLiquidTruncate(param).Truncate(id);
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
