using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentRoom
{
    public partial class HisTreatmentRoomManager : BusinessBase
    {
        public HisTreatmentRoomManager()
            : base()
        {

        }
        
        public HisTreatmentRoomManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TREATMENT_ROOM>> Get(HisTreatmentRoomFilterQuery filter)
        {
            ApiResultObject<List<HIS_TREATMENT_ROOM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TREATMENT_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentRoomGet(param).Get(filter);
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
        public ApiResultObject<HIS_TREATMENT_ROOM> Create(HIS_TREATMENT_ROOM data)
        {
            ApiResultObject<HIS_TREATMENT_ROOM> result = new ApiResultObject<HIS_TREATMENT_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_ROOM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTreatmentRoomCreate(param).Create(data);
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
        public ApiResultObject<HIS_TREATMENT_ROOM> Update(HIS_TREATMENT_ROOM data)
        {
            ApiResultObject<HIS_TREATMENT_ROOM> result = new ApiResultObject<HIS_TREATMENT_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_ROOM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTreatmentRoomUpdate(param).Update(data);
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
        public ApiResultObject<HIS_TREATMENT_ROOM> ChangeLock(long id)
        {
            ApiResultObject<HIS_TREATMENT_ROOM> result = new ApiResultObject<HIS_TREATMENT_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_ROOM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentRoomLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT_ROOM> Lock(long id)
        {
            ApiResultObject<HIS_TREATMENT_ROOM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_ROOM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentRoomLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT_ROOM> Unlock(long id)
        {
            ApiResultObject<HIS_TREATMENT_ROOM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_ROOM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentRoomLock(param).Unlock(id, ref resultData);
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
        public ApiResultObject<List<HIS_TREATMENT_ROOM>> CreateList(List<HIS_TREATMENT_ROOM> listData)
        {
            ApiResultObject<List<HIS_TREATMENT_ROOM>> result = new ApiResultObject<List<HIS_TREATMENT_ROOM>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_TREATMENT_ROOM> resultData = null;
                if (valid && new HisTreatmentRoomCreate(param).CreateList(listData))
                {
                    resultData = listData;
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
                    resultData = new HisTreatmentRoomTruncate(param).Truncate(id);
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
