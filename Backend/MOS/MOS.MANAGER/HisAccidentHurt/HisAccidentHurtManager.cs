using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentHurt
{
    public partial class HisAccidentHurtManager : BusinessBase
    {
        public HisAccidentHurtManager()
            : base()
        {

        }
        
        public HisAccidentHurtManager(CommonParam param)
            : base(param)
        {

        }

		[Logger]
        public ApiResultObject<List<HIS_ACCIDENT_HURT>> Get(HisAccidentHurtFilterQuery filter)
        {
            ApiResultObject<List<HIS_ACCIDENT_HURT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ACCIDENT_HURT> resultData = null;
                if (valid)
                {
                    resultData = new HisAccidentHurtGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_ACCIDENT_HURT>> GetView(HisAccidentHurtViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ACCIDENT_HURT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ACCIDENT_HURT> resultData = null;
                if (valid)
                {
                    resultData = new HisAccidentHurtGet(param).GetView(filter);
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
        public ApiResultObject<HIS_ACCIDENT_HURT> Create(HIS_ACCIDENT_HURT data)
        {
            ApiResultObject<HIS_ACCIDENT_HURT> result = new ApiResultObject<HIS_ACCIDENT_HURT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_HURT resultData = null;
                if (valid && new HisAccidentHurtCreate(param).Create(data))
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
        public ApiResultObject<HIS_ACCIDENT_HURT> Update(HIS_ACCIDENT_HURT data)
        {
            ApiResultObject<HIS_ACCIDENT_HURT> result = new ApiResultObject<HIS_ACCIDENT_HURT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_HURT resultData = null;
                if (valid && new HisAccidentHurtUpdate(param).Update(data))
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
        public ApiResultObject<HIS_ACCIDENT_HURT> Create(HisAccidentHurtSDO data)
        {
            ApiResultObject<HIS_ACCIDENT_HURT> result = new ApiResultObject<HIS_ACCIDENT_HURT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_HURT resultData = null;
                if (valid)
                {
                    new HisAccidentHurtCreateSdo(param).Create(data, ref resultData);
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
        public ApiResultObject<HIS_ACCIDENT_HURT> Update(HisAccidentHurtSDO data)
        {
            ApiResultObject<HIS_ACCIDENT_HURT> result = new ApiResultObject<HIS_ACCIDENT_HURT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_HURT resultData = null;
                if (valid)
                {
                    new HisAccidentHurtUpdateSdo(param).Update(data, ref resultData);
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
        public ApiResultObject<HIS_ACCIDENT_HURT> ChangeLock(HIS_ACCIDENT_HURT data)
        {
            ApiResultObject<HIS_ACCIDENT_HURT> result = new ApiResultObject<HIS_ACCIDENT_HURT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_HURT resultData = null;
                if (valid && new HisAccidentHurtLock(param).ChangeLock(data))
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
                    resultData = new HisAccidentHurtTruncate(param).Truncate(id);
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
