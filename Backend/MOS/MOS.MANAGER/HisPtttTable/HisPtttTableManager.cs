using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttTable
{
    public partial class HisPtttTableManager : BusinessBase
    {
        public HisPtttTableManager()
            : base()
        {

        }
        
        public HisPtttTableManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PTTT_TABLE>> Get(HisPtttTableFilterQuery filter)
        {
            ApiResultObject<List<HIS_PTTT_TABLE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PTTT_TABLE> resultData = null;
                if (valid)
                {
                    resultData = new HisPtttTableGet(param).Get(filter);
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
        public ApiResultObject<HIS_PTTT_TABLE> Create(HIS_PTTT_TABLE data)
        {
            ApiResultObject<HIS_PTTT_TABLE> result = new ApiResultObject<HIS_PTTT_TABLE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_TABLE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPtttTableCreate(param).Create(data);
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
        public ApiResultObject<List<HIS_PTTT_TABLE>> CreateList(List<HIS_PTTT_TABLE> data)
        {
            ApiResultObject<List<HIS_PTTT_TABLE>> result = new ApiResultObject<List<HIS_PTTT_TABLE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_PTTT_TABLE> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttTableCreate(param).CreateList(data);
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
        public ApiResultObject<HIS_PTTT_TABLE> Update(HIS_PTTT_TABLE data)
        {
            ApiResultObject<HIS_PTTT_TABLE> result = new ApiResultObject<HIS_PTTT_TABLE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_TABLE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPtttTableUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PTTT_TABLE> ChangeLock(long id)
        {
            ApiResultObject<HIS_PTTT_TABLE> result = new ApiResultObject<HIS_PTTT_TABLE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_TABLE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttTableLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PTTT_TABLE> Lock(long id)
        {
            ApiResultObject<HIS_PTTT_TABLE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_TABLE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttTableLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PTTT_TABLE> Unlock(long id)
        {
            ApiResultObject<HIS_PTTT_TABLE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_TABLE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttTableLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPtttTableTruncate(param).Truncate(id);
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
