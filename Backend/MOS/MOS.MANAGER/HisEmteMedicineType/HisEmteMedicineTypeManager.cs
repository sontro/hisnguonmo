using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmteMedicineType
{
    public class HisEmteMedicineTypeManager : BusinessBase
    {
        public HisEmteMedicineTypeManager()
            : base()
        {

        }
		
		public HisEmteMedicineTypeManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_EMTE_MEDICINE_TYPE>> Get(HisEmteMedicineTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_EMTE_MEDICINE_TYPE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
				List<HIS_EMTE_MEDICINE_TYPE> resultData = null;
                if (valid)
                {
					resultData = new HisEmteMedicineTypeGet(param).Get(filter);
                }
				result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_EMTE_MEDICINE_TYPE>> GetView(HisEmteMedicineTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EMTE_MEDICINE_TYPE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
				List<V_HIS_EMTE_MEDICINE_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisEmteMedicineTypeGet(param).GetView(filter);
                }
				result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EMTE_MEDICINE_TYPE> Create(HIS_EMTE_MEDICINE_TYPE data)
        {
            ApiResultObject<HIS_EMTE_MEDICINE_TYPE> result = new ApiResultObject<HIS_EMTE_MEDICINE_TYPE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
				HIS_EMTE_MEDICINE_TYPE resultData = null;
                if (valid && new HisEmteMedicineTypeCreate(param).Create(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_EMTE_MEDICINE_TYPE> Update(HIS_EMTE_MEDICINE_TYPE data)
        {
            ApiResultObject<HIS_EMTE_MEDICINE_TYPE> result = new ApiResultObject<HIS_EMTE_MEDICINE_TYPE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
				HIS_EMTE_MEDICINE_TYPE resultData = null;
                if (valid && new HisEmteMedicineTypeUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_EMTE_MEDICINE_TYPE> ChangeLock(HIS_EMTE_MEDICINE_TYPE data)
        {
            ApiResultObject<HIS_EMTE_MEDICINE_TYPE> result = new ApiResultObject<HIS_EMTE_MEDICINE_TYPE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
				HIS_EMTE_MEDICINE_TYPE resultData = null;
                if (valid && new HisEmteMedicineTypeLock(param).ChangeLock(data))
                {
                    resultData = data;
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
        public ApiResultObject<bool> Delete(HIS_EMTE_MEDICINE_TYPE data)
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
                    resultData = new HisEmteMedicineTypeTruncate(param).Truncate(data);
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
    }
}
