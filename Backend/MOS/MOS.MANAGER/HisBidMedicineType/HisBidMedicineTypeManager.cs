using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidMedicineType
{
    public partial class HisBidMedicineTypeManager : BusinessBase
    {
        public HisBidMedicineTypeManager()
            : base()
        {

        }
        
        public HisBidMedicineTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BID_MEDICINE_TYPE>> Get(HisBidMedicineTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_BID_MEDICINE_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BID_MEDICINE_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisBidMedicineTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_BID_MEDICINE_TYPE> Create(HIS_BID_MEDICINE_TYPE data)
        {
            ApiResultObject<HIS_BID_MEDICINE_TYPE> result = new ApiResultObject<HIS_BID_MEDICINE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BID_MEDICINE_TYPE resultData = null;
                if (valid && new HisBidMedicineTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_BID_MEDICINE_TYPE> Update(HIS_BID_MEDICINE_TYPE data)
        {
            ApiResultObject<HIS_BID_MEDICINE_TYPE> result = new ApiResultObject<HIS_BID_MEDICINE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BID_MEDICINE_TYPE resultData = null;
                if (valid && new HisBidMedicineTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_BID_MEDICINE_TYPE> ChangeLock(HIS_BID_MEDICINE_TYPE data)
        {
            ApiResultObject<HIS_BID_MEDICINE_TYPE> result = new ApiResultObject<HIS_BID_MEDICINE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BID_MEDICINE_TYPE resultData = null;
                if (valid && new HisBidMedicineTypeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_BID_MEDICINE_TYPE data)
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
                    resultData = new HisBidMedicineTypeTruncate(param).Truncate(data);
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
