using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmteMaterialType
{
    public class HisEmteMaterialTypeManager : BusinessBase
    {
        public HisEmteMaterialTypeManager()
            : base()
        {

        }
		
		public HisEmteMaterialTypeManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_EMTE_MATERIAL_TYPE>> Get(HisEmteMaterialTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_EMTE_MATERIAL_TYPE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
				List<HIS_EMTE_MATERIAL_TYPE> resultData = null;
                if (valid)
                {
					resultData = new HisEmteMaterialTypeGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_EMTE_MATERIAL_TYPE>> GetView(HisEmteMaterialTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EMTE_MATERIAL_TYPE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
				List<V_HIS_EMTE_MATERIAL_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisEmteMaterialTypeGet(param).GetView(filter);
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
        public ApiResultObject<HIS_EMTE_MATERIAL_TYPE> Create(HIS_EMTE_MATERIAL_TYPE data)
        {
            ApiResultObject<HIS_EMTE_MATERIAL_TYPE> result = new ApiResultObject<HIS_EMTE_MATERIAL_TYPE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
				HIS_EMTE_MATERIAL_TYPE resultData = null;
                if (valid && new HisEmteMaterialTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_EMTE_MATERIAL_TYPE> Update(HIS_EMTE_MATERIAL_TYPE data)
        {
            ApiResultObject<HIS_EMTE_MATERIAL_TYPE> result = new ApiResultObject<HIS_EMTE_MATERIAL_TYPE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
				HIS_EMTE_MATERIAL_TYPE resultData = null;
                if (valid && new HisEmteMaterialTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_EMTE_MATERIAL_TYPE> ChangeLock(HIS_EMTE_MATERIAL_TYPE data)
        {
            ApiResultObject<HIS_EMTE_MATERIAL_TYPE> result = new ApiResultObject<HIS_EMTE_MATERIAL_TYPE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
				HIS_EMTE_MATERIAL_TYPE resultData = null;
                if (valid && new HisEmteMaterialTypeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_EMTE_MATERIAL_TYPE data)
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
                    resultData = new HisEmteMaterialTypeTruncate(param).Truncate(data);
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
