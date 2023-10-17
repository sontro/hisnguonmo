using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAnticipateBlty
{
    public partial class HisAnticipateBltyManager : BusinessBase
    {
        public HisAnticipateBltyManager()
            : base()
        {

        }
        
        public HisAnticipateBltyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ANTICIPATE_BLTY>> Get(HisAnticipateBltyFilterQuery filter)
        {
            ApiResultObject<List<HIS_ANTICIPATE_BLTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ANTICIPATE_BLTY> resultData = null;
                if (valid)
                {
                    resultData = new HisAnticipateBltyGet(param).Get(filter);
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
        public ApiResultObject<HIS_ANTICIPATE_BLTY> Create(HIS_ANTICIPATE_BLTY data)
        {
            ApiResultObject<HIS_ANTICIPATE_BLTY> result = new ApiResultObject<HIS_ANTICIPATE_BLTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTICIPATE_BLTY resultData = null;
                if (valid && new HisAnticipateBltyCreate(param).Create(data))
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
        public ApiResultObject<HIS_ANTICIPATE_BLTY> Update(HIS_ANTICIPATE_BLTY data)
        {
            ApiResultObject<HIS_ANTICIPATE_BLTY> result = new ApiResultObject<HIS_ANTICIPATE_BLTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTICIPATE_BLTY resultData = null;
                if (valid && new HisAnticipateBltyUpdate(param).Update(data))
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
        public ApiResultObject<HIS_ANTICIPATE_BLTY> ChangeLock(long id)
        {
            ApiResultObject<HIS_ANTICIPATE_BLTY> result = new ApiResultObject<HIS_ANTICIPATE_BLTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTICIPATE_BLTY resultData = null;
                if (valid)
                {
                    new HisAnticipateBltyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTICIPATE_BLTY> Lock(long id)
        {
            ApiResultObject<HIS_ANTICIPATE_BLTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTICIPATE_BLTY resultData = null;
                if (valid)
                {
                    new HisAnticipateBltyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTICIPATE_BLTY> Unlock(long id)
        {
            ApiResultObject<HIS_ANTICIPATE_BLTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTICIPATE_BLTY resultData = null;
                if (valid)
                {
                    new HisAnticipateBltyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAnticipateBltyTruncate(param).Truncate(id);
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
