using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEquipmentSetMaty
{
    public partial class HisEquipmentSetMatyManager : BusinessBase
    {
        public HisEquipmentSetMatyManager()
            : base()
        {

        }
        
        public HisEquipmentSetMatyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EQUIPMENT_SET_MATY>> Get(HisEquipmentSetMatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_EQUIPMENT_SET_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EQUIPMENT_SET_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisEquipmentSetMatyGet(param).Get(filter);
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
        public ApiResultObject<HIS_EQUIPMENT_SET_MATY> Create(HIS_EQUIPMENT_SET_MATY data)
        {
            ApiResultObject<HIS_EQUIPMENT_SET_MATY> result = new ApiResultObject<HIS_EQUIPMENT_SET_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EQUIPMENT_SET_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEquipmentSetMatyCreate(param).Create(data);
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
        public ApiResultObject<HIS_EQUIPMENT_SET_MATY> Update(HIS_EQUIPMENT_SET_MATY data)
        {
            ApiResultObject<HIS_EQUIPMENT_SET_MATY> result = new ApiResultObject<HIS_EQUIPMENT_SET_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EQUIPMENT_SET_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEquipmentSetMatyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EQUIPMENT_SET_MATY> ChangeLock(long id)
        {
            ApiResultObject<HIS_EQUIPMENT_SET_MATY> result = new ApiResultObject<HIS_EQUIPMENT_SET_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EQUIPMENT_SET_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEquipmentSetMatyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EQUIPMENT_SET_MATY> Lock(long id)
        {
            ApiResultObject<HIS_EQUIPMENT_SET_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EQUIPMENT_SET_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEquipmentSetMatyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EQUIPMENT_SET_MATY> Unlock(long id)
        {
            ApiResultObject<HIS_EQUIPMENT_SET_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EQUIPMENT_SET_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEquipmentSetMatyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisEquipmentSetMatyTruncate(param).Truncate(id);
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
        public ApiResultObject<List<HIS_EQUIPMENT_SET_MATY>> CreateList(List<HIS_EQUIPMENT_SET_MATY> listData)
        {
            ApiResultObject<List<HIS_EQUIPMENT_SET_MATY>> result = new ApiResultObject<List<HIS_EQUIPMENT_SET_MATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_EQUIPMENT_SET_MATY> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEquipmentSetMatyCreate(param).CreateList(listData);
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
        public ApiResultObject<List<HIS_EQUIPMENT_SET_MATY>> UpdateList(List<HIS_EQUIPMENT_SET_MATY> listData)
        {
            ApiResultObject<List<HIS_EQUIPMENT_SET_MATY>> result = new ApiResultObject<List<HIS_EQUIPMENT_SET_MATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_EQUIPMENT_SET_MATY> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEquipmentSetMatyUpdate(param).UpdateList(listData);
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
                    resultData = new HisEquipmentSetMatyTruncate(param).TruncateList(ids);
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
