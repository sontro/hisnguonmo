using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareType
{
    public partial class HisCareTypeManager : BusinessBase
    {
        public HisCareTypeManager()
            : base()
        {

        }
        
        public HisCareTypeManager(CommonParam param)
            : base(param)
        {

        }

		[Logger]
        public ApiResultObject<List<HIS_CARE_TYPE>> Get(HisCareTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_CARE_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CARE_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisCareTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_CARE_TYPE> Create(HIS_CARE_TYPE data)
        {
            ApiResultObject<HIS_CARE_TYPE> result = new ApiResultObject<HIS_CARE_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_TYPE resultData = null;
                if (valid && new HisCareTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_CARE_TYPE> Update(HIS_CARE_TYPE data)
        {
            ApiResultObject<HIS_CARE_TYPE> result = new ApiResultObject<HIS_CARE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_TYPE resultData = null;
                if (valid && new HisCareTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_CARE_TYPE> ChangeLock(HIS_CARE_TYPE data)
        {
            ApiResultObject<HIS_CARE_TYPE> result = new ApiResultObject<HIS_CARE_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_TYPE resultData = null;
                if (valid && new HisCareTypeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_CARE_TYPE data)
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
                    resultData = new HisCareTypeTruncate(param).Truncate(data);
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
