using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteGroup
{
    public partial class HisExecuteGroupManager : BusinessBase
    {
        public HisExecuteGroupManager()
            : base()
        {

        }
        
        public HisExecuteGroupManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EXECUTE_GROUP>> Get(HisExecuteGroupFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXECUTE_GROUP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXECUTE_GROUP> resultData = null;
                if (valid)
                {
                    resultData = new HisExecuteGroupGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXECUTE_GROUP> Create(HIS_EXECUTE_GROUP data)
        {
            ApiResultObject<HIS_EXECUTE_GROUP> result = new ApiResultObject<HIS_EXECUTE_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXECUTE_GROUP resultData = null;
                if (valid && new HisExecuteGroupCreate(param).Create(data))
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
        public ApiResultObject<HIS_EXECUTE_GROUP> Update(HIS_EXECUTE_GROUP data)
        {
            ApiResultObject<HIS_EXECUTE_GROUP> result = new ApiResultObject<HIS_EXECUTE_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXECUTE_GROUP resultData = null;
                if (valid && new HisExecuteGroupUpdate(param).Update(data))
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
        public ApiResultObject<HIS_EXECUTE_GROUP> ChangeLock(HIS_EXECUTE_GROUP data)
        {
            ApiResultObject<HIS_EXECUTE_GROUP> result = new ApiResultObject<HIS_EXECUTE_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXECUTE_GROUP resultData = null;
                if (valid && new HisExecuteGroupLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_EXECUTE_GROUP data)
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
                    resultData = new HisExecuteGroupTruncate(param).Truncate(data);
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
