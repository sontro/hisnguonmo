using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialTypeMap
{
    public partial class HisMaterialTypeMapManager : BusinessBase
    {
        public HisMaterialTypeMapManager()
            : base()
        {

        }
        
        public HisMaterialTypeMapManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MATERIAL_TYPE_MAP>> Get(HisMaterialTypeMapFilterQuery filter)
        {
            ApiResultObject<List<HIS_MATERIAL_TYPE_MAP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MATERIAL_TYPE_MAP> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialTypeMapGet(param).Get(filter);
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
        public ApiResultObject<HIS_MATERIAL_TYPE_MAP> Create(HIS_MATERIAL_TYPE_MAP data)
        {
            ApiResultObject<HIS_MATERIAL_TYPE_MAP> result = new ApiResultObject<HIS_MATERIAL_TYPE_MAP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_TYPE_MAP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMaterialTypeMapCreate(param).Create(data);
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
        public ApiResultObject<HIS_MATERIAL_TYPE_MAP> Update(HIS_MATERIAL_TYPE_MAP data)
        {
            ApiResultObject<HIS_MATERIAL_TYPE_MAP> result = new ApiResultObject<HIS_MATERIAL_TYPE_MAP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_TYPE_MAP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMaterialTypeMapUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MATERIAL_TYPE_MAP> ChangeLock(long id)
        {
            ApiResultObject<HIS_MATERIAL_TYPE_MAP> result = new ApiResultObject<HIS_MATERIAL_TYPE_MAP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MATERIAL_TYPE_MAP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMaterialTypeMapLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MATERIAL_TYPE_MAP> Lock(long id)
        {
            ApiResultObject<HIS_MATERIAL_TYPE_MAP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MATERIAL_TYPE_MAP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMaterialTypeMapLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MATERIAL_TYPE_MAP> Unlock(long id)
        {
            ApiResultObject<HIS_MATERIAL_TYPE_MAP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MATERIAL_TYPE_MAP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMaterialTypeMapLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMaterialTypeMapTruncate(param).Truncate(id);
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
