using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineTypeTut
{
    public partial class HisMedicineTypeTutManager : BusinessBase
    {
        public HisMedicineTypeTutManager()
            : base()
        {

        }
        
        public HisMedicineTypeTutManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDICINE_TYPE_TUT>> Get(HisMedicineTypeTutFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICINE_TYPE_TUT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICINE_TYPE_TUT> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineTypeTutGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDICINE_TYPE_TUT> Create(HIS_MEDICINE_TYPE_TUT data)
        {
            ApiResultObject<HIS_MEDICINE_TYPE_TUT> result = new ApiResultObject<HIS_MEDICINE_TYPE_TUT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_TYPE_TUT resultData = null;
                if (valid && new HisMedicineTypeTutCreate(param).Create(data))
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
        public ApiResultObject<HIS_MEDICINE_TYPE_TUT> Update(HIS_MEDICINE_TYPE_TUT data)
        {
            ApiResultObject<HIS_MEDICINE_TYPE_TUT> result = new ApiResultObject<HIS_MEDICINE_TYPE_TUT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_TYPE_TUT resultData = null;
                if (valid && new HisMedicineTypeTutUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEDICINE_TYPE_TUT> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDICINE_TYPE_TUT> result = new ApiResultObject<HIS_MEDICINE_TYPE_TUT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_TYPE_TUT resultData = null;
                if (valid)
                {
                    new HisMedicineTypeTutLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICINE_TYPE_TUT> Lock(long id)
        {
            ApiResultObject<HIS_MEDICINE_TYPE_TUT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_TYPE_TUT resultData = null;
                if (valid)
                {
                    new HisMedicineTypeTutLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICINE_TYPE_TUT> Unlock(long id)
        {
            ApiResultObject<HIS_MEDICINE_TYPE_TUT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_TYPE_TUT resultData = null;
                if (valid)
                {
                    new HisMedicineTypeTutLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMedicineTypeTutTruncate(param).Truncate(id);
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
