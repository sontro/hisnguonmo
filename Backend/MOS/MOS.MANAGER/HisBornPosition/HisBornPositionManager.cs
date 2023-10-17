using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBornPosition
{
    public partial class HisBornPositionManager : BusinessBase
    {
        public HisBornPositionManager()
            : base()
        {

        }
        
        public HisBornPositionManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BORN_POSITION>> Get(HisBornPositionFilterQuery filter)
        {
            ApiResultObject<List<HIS_BORN_POSITION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BORN_POSITION> resultData = null;
                if (valid)
                {
                    resultData = new HisBornPositionGet(param).Get(filter);
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
        public ApiResultObject<HIS_BORN_POSITION> Create(HIS_BORN_POSITION data)
        {
            ApiResultObject<HIS_BORN_POSITION> result = new ApiResultObject<HIS_BORN_POSITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BORN_POSITION resultData = null;
                if (valid && new HisBornPositionCreate(param).Create(data))
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
        public ApiResultObject<HIS_BORN_POSITION> Update(HIS_BORN_POSITION data)
        {
            ApiResultObject<HIS_BORN_POSITION> result = new ApiResultObject<HIS_BORN_POSITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BORN_POSITION resultData = null;
                if (valid && new HisBornPositionUpdate(param).Update(data))
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
        public ApiResultObject<HIS_BORN_POSITION> ChangeLock(long id)
        {
            ApiResultObject<HIS_BORN_POSITION> result = new ApiResultObject<HIS_BORN_POSITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BORN_POSITION resultData = null;
                if (valid)
                {
                    new HisBornPositionLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BORN_POSITION> Lock(long id)
        {
            ApiResultObject<HIS_BORN_POSITION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BORN_POSITION resultData = null;
                if (valid)
                {
                    new HisBornPositionLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BORN_POSITION> Unlock(long id)
        {
            ApiResultObject<HIS_BORN_POSITION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BORN_POSITION resultData = null;
                if (valid)
                {
                    new HisBornPositionLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBornPositionTruncate(param).Truncate(id);
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
