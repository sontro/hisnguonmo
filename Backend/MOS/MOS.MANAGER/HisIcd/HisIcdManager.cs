using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisIcd
{
    public partial class HisIcdManager : BusinessBase
    {
        public HisIcdManager()
            : base()
        {

        }
        
        public HisIcdManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_ICD>> Get(HisIcdFilterQuery filter)
        {
            ApiResultObject<List<HIS_ICD>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ICD> resultData = null;
                if (valid)
                {
                    resultData = new HisIcdGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_ICD> GetById(long id)
        {
            ApiResultObject<HIS_ICD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_ICD resultData = null;
                if (valid)
                {
                    HisIcdFilterQuery filter = new HisIcdFilterQuery();
                    resultData = new HisIcdGet(param).GetById(id, filter);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_ICD> Create(HIS_ICD data)
        {
            ApiResultObject<HIS_ICD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ICD resultData = null;
                if (valid && new HisIcdCreate(param).Create(data))
                {
                    resultData = data;
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
        public ApiResultObject<List<HIS_ICD>> CreateList(List<HIS_ICD> data)
        {
            ApiResultObject<List<HIS_ICD>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_ICD> resultData = null;
                if (valid && new HisIcdCreate(param).CreateList(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_ICD> Update(HIS_ICD data)
        {
            ApiResultObject<HIS_ICD> result = new ApiResultObject<HIS_ICD>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ICD resultData = null;
                if (valid && new HisIcdUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_ICD> ChangeLock(HIS_ICD data)
        {
            ApiResultObject<HIS_ICD> result = new ApiResultObject<HIS_ICD>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_ICD resultData = null;
                if (valid && new HisIcdLock(param).ChangeLock(data))
                {
                    resultData = data;
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
        public ApiResultObject<bool> Delete(HIS_ICD data)
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
                    resultData = new HisIcdTruncate(param).Truncate(data);
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
    }
}
