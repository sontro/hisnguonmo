using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEquipmentSet
{
    public partial class HisEquipmentSetManager : BusinessBase
    {
        public HisEquipmentSetManager()
            : base()
        {

        }
        
        public HisEquipmentSetManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EQUIPMENT_SET>> Get(HisEquipmentSetFilterQuery filter)
        {
            ApiResultObject<List<HIS_EQUIPMENT_SET>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EQUIPMENT_SET> resultData = null;
                if (valid)
                {
                    resultData = new HisEquipmentSetGet(param).Get(filter);
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
        public ApiResultObject<HIS_EQUIPMENT_SET> Create(HIS_EQUIPMENT_SET data)
        {
            ApiResultObject<HIS_EQUIPMENT_SET> result = new ApiResultObject<HIS_EQUIPMENT_SET>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EQUIPMENT_SET resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEquipmentSetCreate(param).Create(data);
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
        public ApiResultObject<HIS_EQUIPMENT_SET> Update(HIS_EQUIPMENT_SET data)
        {
            ApiResultObject<HIS_EQUIPMENT_SET> result = new ApiResultObject<HIS_EQUIPMENT_SET>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EQUIPMENT_SET resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEquipmentSetUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EQUIPMENT_SET> ChangeLock(long id)
        {
            ApiResultObject<HIS_EQUIPMENT_SET> result = new ApiResultObject<HIS_EQUIPMENT_SET>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EQUIPMENT_SET resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEquipmentSetLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EQUIPMENT_SET> Lock(long id)
        {
            ApiResultObject<HIS_EQUIPMENT_SET> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EQUIPMENT_SET resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEquipmentSetLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EQUIPMENT_SET> Unlock(long id)
        {
            ApiResultObject<HIS_EQUIPMENT_SET> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EQUIPMENT_SET resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEquipmentSetLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisEquipmentSetTruncate(param).Truncate(id);
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
        public ApiResultObject<List<HIS_EQUIPMENT_SET>> CreateList(List<HIS_EQUIPMENT_SET> listData)
        {
            ApiResultObject<List<HIS_EQUIPMENT_SET>> result = new ApiResultObject<List<HIS_EQUIPMENT_SET>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_EQUIPMENT_SET> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEquipmentSetCreate(param).CreateList(listData);
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
        public ApiResultObject<List<HIS_EQUIPMENT_SET>> UpdateList(List<HIS_EQUIPMENT_SET> listData)
        {
            ApiResultObject<List<HIS_EQUIPMENT_SET>> result = new ApiResultObject<List<HIS_EQUIPMENT_SET>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_EQUIPMENT_SET> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEquipmentSetUpdate(param).UpdateList(listData);
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

    }
}
