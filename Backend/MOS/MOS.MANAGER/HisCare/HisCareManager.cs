using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCare
{
    public partial class HisCareManager : BusinessBase
    {
        public HisCareManager()
            : base()
        {

        }
        
        public HisCareManager(CommonParam param)
            : base(param)
        {

        }

		[Logger]
        public ApiResultObject<List<HIS_CARE>> Get(HisCareFilterQuery filter)
        {
            ApiResultObject<List<HIS_CARE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CARE> resultData = null;
                if (valid)
                {
                    resultData = new HisCareGet(param).Get(filter);
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
        public ApiResultObject<HIS_CARE> Create(HIS_CARE data)
        {
            ApiResultObject<HIS_CARE> result = new ApiResultObject<HIS_CARE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE resultData = null;
                if (valid && new HisCareCreate(param).Create(data))
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
        public ApiResultObject<HIS_CARE> Update(HIS_CARE data)
        {
            ApiResultObject<HIS_CARE> result = new ApiResultObject<HIS_CARE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE resultData = null;
                if (valid && new HisCareUpdate(param).Update(data))
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
        public ApiResultObject<HIS_CARE> UpdateWithDhst(HIS_CARE data)
        {
            ApiResultObject<HIS_CARE> result = new ApiResultObject<HIS_CARE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE resultData = null;
                if (valid && new HisCareUpdateWithDhst(param).Run(data))
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
        public ApiResultObject<HIS_CARE> ChangeLock(HIS_CARE data)
        {
            ApiResultObject<HIS_CARE> result = new ApiResultObject<HIS_CARE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE resultData = null;
                if (valid && new HisCareLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_CARE data)
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
                    resultData = new HisCareTruncate(param).Truncate(data);
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
