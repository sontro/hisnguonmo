using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPaanPosition
{
    public partial class HisPaanPositionManager : BusinessBase
    {
        public HisPaanPositionManager()
            : base()
        {

        }
        
        public HisPaanPositionManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PAAN_POSITION>> Get(HisPaanPositionFilterQuery filter)
        {
            ApiResultObject<List<HIS_PAAN_POSITION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PAAN_POSITION> resultData = null;
                if (valid)
                {
                    resultData = new HisPaanPositionGet(param).Get(filter);
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
        public ApiResultObject<HIS_PAAN_POSITION> Create(HIS_PAAN_POSITION data)
        {
            ApiResultObject<HIS_PAAN_POSITION> result = new ApiResultObject<HIS_PAAN_POSITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PAAN_POSITION resultData = null;
                if (valid && new HisPaanPositionCreate(param).Create(data))
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
        public ApiResultObject<HIS_PAAN_POSITION> Update(HIS_PAAN_POSITION data)
        {
            ApiResultObject<HIS_PAAN_POSITION> result = new ApiResultObject<HIS_PAAN_POSITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PAAN_POSITION resultData = null;
                if (valid && new HisPaanPositionUpdate(param).Update(data))
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
        public ApiResultObject<HIS_PAAN_POSITION> ChangeLock(long id)
        {
            ApiResultObject<HIS_PAAN_POSITION> result = new ApiResultObject<HIS_PAAN_POSITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PAAN_POSITION resultData = null;
                if (valid)
                {
                    new HisPaanPositionLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PAAN_POSITION> Lock(long id)
        {
            ApiResultObject<HIS_PAAN_POSITION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PAAN_POSITION resultData = null;
                if (valid)
                {
                    new HisPaanPositionLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PAAN_POSITION> Unlock(long id)
        {
            ApiResultObject<HIS_PAAN_POSITION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PAAN_POSITION resultData = null;
                if (valid)
                {
                    new HisPaanPositionLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPaanPositionTruncate(param).Truncate(id);
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
