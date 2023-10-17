using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeRoom
{
    public partial class HisPatientTypeRoomManager : BusinessBase
    {
        public HisPatientTypeRoomManager()
            : base()
        {

        }
        
        public HisPatientTypeRoomManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PATIENT_TYPE_ROOM>> Get(HisPatientTypeRoomFilterQuery filter)
        {
            ApiResultObject<List<HIS_PATIENT_TYPE_ROOM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PATIENT_TYPE_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientTypeRoomGet(param).Get(filter);
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
        public ApiResultObject<HIS_PATIENT_TYPE_ROOM> Create(HIS_PATIENT_TYPE_ROOM data)
        {
            ApiResultObject<HIS_PATIENT_TYPE_ROOM> result = new ApiResultObject<HIS_PATIENT_TYPE_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_TYPE_ROOM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPatientTypeRoomCreate(param).Create(data);
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
        public ApiResultObject<List<HIS_PATIENT_TYPE_ROOM>> CreateList(List<HIS_PATIENT_TYPE_ROOM> listData)
        {
            ApiResultObject<List<HIS_PATIENT_TYPE_ROOM>> result = new ApiResultObject<List<HIS_PATIENT_TYPE_ROOM>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_PATIENT_TYPE_ROOM> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPatientTypeRoomCreate(param).CreateList(listData);
                    resultData = isSuccess ? listData : null;
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
        public ApiResultObject<HIS_PATIENT_TYPE_ROOM> Update(HIS_PATIENT_TYPE_ROOM data)
        {
            ApiResultObject<HIS_PATIENT_TYPE_ROOM> result = new ApiResultObject<HIS_PATIENT_TYPE_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_TYPE_ROOM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPatientTypeRoomUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PATIENT_TYPE_ROOM> ChangeLock(long id)
        {
            ApiResultObject<HIS_PATIENT_TYPE_ROOM> result = new ApiResultObject<HIS_PATIENT_TYPE_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PATIENT_TYPE_ROOM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPatientTypeRoomLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PATIENT_TYPE_ROOM> Lock(long id)
        {
            ApiResultObject<HIS_PATIENT_TYPE_ROOM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PATIENT_TYPE_ROOM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPatientTypeRoomLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PATIENT_TYPE_ROOM> Unlock(long id)
        {
            ApiResultObject<HIS_PATIENT_TYPE_ROOM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PATIENT_TYPE_ROOM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPatientTypeRoomLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPatientTypeRoomTruncate(param).Truncate(id);
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
        public ApiResultObject<bool> DeleteList(List<long> ids)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisPatientTypeRoomTruncate(param).TruncateList(ids);
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
