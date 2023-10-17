using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttGroup
{
    public partial class HisPtttGroupManager : BusinessBase
    {
        public HisPtttGroupManager()
            : base()
        {

        }
        
        public HisPtttGroupManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PTTT_GROUP>> Get(HisPtttGroupFilterQuery filter)
        {
            ApiResultObject<List<HIS_PTTT_GROUP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PTTT_GROUP> resultData = null;
                if (valid)
                {
                    resultData = new HisPtttGroupGet(param).Get(filter);
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
        public ApiResultObject<HisPtttGroupSDO> Create(HisPtttGroupSDO data)
        {
            ApiResultObject<HisPtttGroupSDO> result = new ApiResultObject<HisPtttGroupSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisPtttGroupSDO resultData = null;
                if (valid && new HisPtttGroupCreate(param).Create(data))
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
        public ApiResultObject<HisPtttGroupSDO> Update(HisPtttGroupSDO data)
        {
            ApiResultObject<HisPtttGroupSDO> result = new ApiResultObject<HisPtttGroupSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisPtttGroupSDO resultData = null;
                if (valid && new HisPtttGroupUpdate(param).Update(data))
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
        public ApiResultObject<HIS_PTTT_GROUP> ChangeLock(HIS_PTTT_GROUP data)
        {
            ApiResultObject<HIS_PTTT_GROUP> result = new ApiResultObject<HIS_PTTT_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_GROUP resultData = null;
                if (valid && new HisPtttGroupLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_PTTT_GROUP data)
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
                    resultData = new HisPtttGroupTruncate(param).Truncate(data);
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
