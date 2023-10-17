using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineGroup
{
    public partial class HisMedicineGroupManager : BusinessBase
    {
        public HisMedicineGroupManager()
            : base()
        {

        }
        
        public HisMedicineGroupManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDICINE_GROUP>> Get(HisMedicineGroupFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICINE_GROUP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICINE_GROUP> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineGroupGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDICINE_GROUP> Create(HIS_MEDICINE_GROUP data)
        {
            ApiResultObject<HIS_MEDICINE_GROUP> result = new ApiResultObject<HIS_MEDICINE_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMedicineGroupCreate(param).Create(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_MEDICINE_GROUP> Update(HIS_MEDICINE_GROUP data)
        {
            ApiResultObject<HIS_MEDICINE_GROUP> result = new ApiResultObject<HIS_MEDICINE_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMedicineGroupUpdate(param).Update(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_MEDICINE_GROUP> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDICINE_GROUP> result = new ApiResultObject<HIS_MEDICINE_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineGroupLock(param).ChangeLock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_MEDICINE_GROUP> Lock(long id)
        {
            ApiResultObject<HIS_MEDICINE_GROUP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineGroupLock(param).Lock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_MEDICINE_GROUP> Unlock(long id)
        {
            ApiResultObject<HIS_MEDICINE_GROUP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineGroupLock(param).Unlock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
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
                    resultData = new HisMedicineGroupTruncate(param).Truncate(id);
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
