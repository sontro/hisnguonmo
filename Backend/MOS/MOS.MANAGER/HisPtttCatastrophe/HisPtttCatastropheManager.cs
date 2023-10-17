using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttCatastrophe
{
    public partial class HisPtttCatastropheManager : BusinessBase
    {
        public HisPtttCatastropheManager()
            : base()
        {

        }
        
        public HisPtttCatastropheManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PTTT_CATASTROPHE>> Get(HisPtttCatastropheFilterQuery filter)
        {
            ApiResultObject<List<HIS_PTTT_CATASTROPHE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PTTT_CATASTROPHE> resultData = null;
                if (valid)
                {
                    resultData = new HisPtttCatastropheGet(param).Get(filter);
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
        public ApiResultObject<HIS_PTTT_CATASTROPHE> Create(HIS_PTTT_CATASTROPHE data)
        {
            ApiResultObject<HIS_PTTT_CATASTROPHE> result = new ApiResultObject<HIS_PTTT_CATASTROPHE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_CATASTROPHE resultData = null;
                if (valid && new HisPtttCatastropheCreate(param).Create(data))
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
        public ApiResultObject<HIS_PTTT_CATASTROPHE> Update(HIS_PTTT_CATASTROPHE data)
        {
            ApiResultObject<HIS_PTTT_CATASTROPHE> result = new ApiResultObject<HIS_PTTT_CATASTROPHE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_CATASTROPHE resultData = null;
                if (valid && new HisPtttCatastropheUpdate(param).Update(data))
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
        public ApiResultObject<HIS_PTTT_CATASTROPHE> ChangeLock(HIS_PTTT_CATASTROPHE data)
        {
            ApiResultObject<HIS_PTTT_CATASTROPHE> result = new ApiResultObject<HIS_PTTT_CATASTROPHE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_CATASTROPHE resultData = null;
                if (valid && new HisPtttCatastropheLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_PTTT_CATASTROPHE data)
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
                    resultData = new HisPtttCatastropheTruncate(param).Truncate(data);
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
