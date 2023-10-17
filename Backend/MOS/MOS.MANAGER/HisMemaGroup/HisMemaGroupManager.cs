using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMemaGroup
{
    public partial class HisMemaGroupManager : BusinessBase
    {
        public HisMemaGroupManager()
            : base()
        {

        }
        
        public HisMemaGroupManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEMA_GROUP>> Get(HisMemaGroupFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEMA_GROUP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEMA_GROUP> resultData = null;
                if (valid)
                {
                    resultData = new HisMemaGroupGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEMA_GROUP> Create(HIS_MEMA_GROUP data)
        {
            ApiResultObject<HIS_MEMA_GROUP> result = new ApiResultObject<HIS_MEMA_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEMA_GROUP resultData = null;
                if (valid && new HisMemaGroupCreate(param).Create(data))
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
        public ApiResultObject<HIS_MEMA_GROUP> Update(HIS_MEMA_GROUP data)
        {
            ApiResultObject<HIS_MEMA_GROUP> result = new ApiResultObject<HIS_MEMA_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEMA_GROUP resultData = null;
                if (valid && new HisMemaGroupUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEMA_GROUP> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEMA_GROUP> result = new ApiResultObject<HIS_MEMA_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEMA_GROUP resultData = null;
                if (valid)
                {
                    new HisMemaGroupLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEMA_GROUP> Lock(long id)
        {
            ApiResultObject<HIS_MEMA_GROUP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEMA_GROUP resultData = null;
                if (valid)
                {
                    new HisMemaGroupLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEMA_GROUP> Unlock(long id)
        {
            ApiResultObject<HIS_MEMA_GROUP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEMA_GROUP resultData = null;
                if (valid)
                {
                    new HisMemaGroupLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMemaGroupTruncate(param).Truncate(id);
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
