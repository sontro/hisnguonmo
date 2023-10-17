using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentBodyPart
{
    public partial class HisAccidentBodyPartManager : BusinessBase
    {
        public HisAccidentBodyPartManager()
            : base()
        {

        }
        
        public HisAccidentBodyPartManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ACCIDENT_BODY_PART>> Get(HisAccidentBodyPartFilterQuery filter)
        {
            ApiResultObject<List<HIS_ACCIDENT_BODY_PART>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ACCIDENT_BODY_PART> resultData = null;
                if (valid)
                {
                    resultData = new HisAccidentBodyPartGet(param).Get(filter);
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
        public ApiResultObject<HIS_ACCIDENT_BODY_PART> Create(HIS_ACCIDENT_BODY_PART data)
        {
            ApiResultObject<HIS_ACCIDENT_BODY_PART> result = new ApiResultObject<HIS_ACCIDENT_BODY_PART>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_BODY_PART resultData = null;
                if (valid && new HisAccidentBodyPartCreate(param).Create(data))
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
        public ApiResultObject<HIS_ACCIDENT_BODY_PART> Update(HIS_ACCIDENT_BODY_PART data)
        {
            ApiResultObject<HIS_ACCIDENT_BODY_PART> result = new ApiResultObject<HIS_ACCIDENT_BODY_PART>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_BODY_PART resultData = null;
                if (valid && new HisAccidentBodyPartUpdate(param).Update(data))
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
        public ApiResultObject<HIS_ACCIDENT_BODY_PART> ChangeLock(HIS_ACCIDENT_BODY_PART data)
        {
            ApiResultObject<HIS_ACCIDENT_BODY_PART> result = new ApiResultObject<HIS_ACCIDENT_BODY_PART>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_BODY_PART resultData = null;
                if (valid && new HisAccidentBodyPartLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_ACCIDENT_BODY_PART data)
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
                    resultData = new HisAccidentBodyPartTruncate(param).Truncate(data);
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
