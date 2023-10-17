using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidMaterialType
{
    public partial class HisBidMaterialTypeManager : BusinessBase
    {
        public HisBidMaterialTypeManager()
            : base()
        {

        }
        
        public HisBidMaterialTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BID_MATERIAL_TYPE>> Get(HisBidMaterialTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_BID_MATERIAL_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BID_MATERIAL_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisBidMaterialTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_BID_MATERIAL_TYPE> Create(HIS_BID_MATERIAL_TYPE data)
        {
            ApiResultObject<HIS_BID_MATERIAL_TYPE> result = new ApiResultObject<HIS_BID_MATERIAL_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BID_MATERIAL_TYPE resultData = null;
                if (valid && new HisBidMaterialTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_BID_MATERIAL_TYPE> Update(HIS_BID_MATERIAL_TYPE data)
        {
            ApiResultObject<HIS_BID_MATERIAL_TYPE> result = new ApiResultObject<HIS_BID_MATERIAL_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BID_MATERIAL_TYPE resultData = null;
                if (valid && new HisBidMaterialTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_BID_MATERIAL_TYPE> ChangeLock(HIS_BID_MATERIAL_TYPE data)
        {
            ApiResultObject<HIS_BID_MATERIAL_TYPE> result = new ApiResultObject<HIS_BID_MATERIAL_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BID_MATERIAL_TYPE resultData = null;
                if (valid && new HisBidMaterialTypeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_BID_MATERIAL_TYPE data)
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
                    resultData = new HisBidMaterialTypeTruncate(param).Truncate(data);
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
