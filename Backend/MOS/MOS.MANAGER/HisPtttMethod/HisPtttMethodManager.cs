using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttMethod
{
    public partial class HisPtttMethodManager : BusinessBase
    {
        public HisPtttMethodManager()
            : base()
        {

        }
        
        public HisPtttMethodManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PTTT_METHOD>> Get(HisPtttMethodFilterQuery filter)
        {
            ApiResultObject<List<HIS_PTTT_METHOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PTTT_METHOD> resultData = null;
                if (valid)
                {
                    resultData = new HisPtttMethodGet(param).Get(filter);
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
        public ApiResultObject<HIS_PTTT_METHOD> Create(HIS_PTTT_METHOD data)
        {
            ApiResultObject<HIS_PTTT_METHOD> result = new ApiResultObject<HIS_PTTT_METHOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_METHOD resultData = null;
                if (valid && new HisPtttMethodCreate(param).Create(data))
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
        public ApiResultObject<HIS_PTTT_METHOD> Update(HIS_PTTT_METHOD data)
        {
            ApiResultObject<HIS_PTTT_METHOD> result = new ApiResultObject<HIS_PTTT_METHOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_METHOD resultData = null;
                if (valid && new HisPtttMethodUpdate(param).Update(data))
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
        public ApiResultObject<HIS_PTTT_METHOD> ChangeLock(HIS_PTTT_METHOD data)
        {
            ApiResultObject<HIS_PTTT_METHOD> result = new ApiResultObject<HIS_PTTT_METHOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_METHOD resultData = null;
                if (valid && new HisPtttMethodLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_PTTT_METHOD data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisPtttMethodTruncate(param).Truncate(data);
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
        public ApiResultObject<List<HIS_PTTT_METHOD>> CreateList(List<HIS_PTTT_METHOD> listData)
        {
            ApiResultObject<List<HIS_PTTT_METHOD>> result = new ApiResultObject<List<HIS_PTTT_METHOD>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_PTTT_METHOD> resultData = null;
                if (valid && new HisPtttMethodCreate(param).CreateList(listData))
                {
                    resultData = listData;
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
