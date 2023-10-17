using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialMaterial
{
    public partial class HisMaterialMaterialManager : BusinessBase
    {
        public HisMaterialMaterialManager()
            : base()
        {

        }
        
        public HisMaterialMaterialManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MATERIAL_MATERIAL>> Get(HisMaterialMaterialFilterQuery filter)
        {
            ApiResultObject<List<HIS_MATERIAL_MATERIAL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MATERIAL_MATERIAL> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialMaterialGet(param).Get(filter);
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
        public ApiResultObject<HIS_MATERIAL_MATERIAL> Create(HIS_MATERIAL_MATERIAL data)
        {
            ApiResultObject<HIS_MATERIAL_MATERIAL> result = new ApiResultObject<HIS_MATERIAL_MATERIAL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_MATERIAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMaterialMaterialCreate(param).Create(data);
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
        public ApiResultObject<HIS_MATERIAL_MATERIAL> Update(HIS_MATERIAL_MATERIAL data)
        {
            ApiResultObject<HIS_MATERIAL_MATERIAL> result = new ApiResultObject<HIS_MATERIAL_MATERIAL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_MATERIAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMaterialMaterialUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MATERIAL_MATERIAL> ChangeLock(long id)
        {
            ApiResultObject<HIS_MATERIAL_MATERIAL> result = new ApiResultObject<HIS_MATERIAL_MATERIAL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MATERIAL_MATERIAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMaterialMaterialLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MATERIAL_MATERIAL> Lock(long id)
        {
            ApiResultObject<HIS_MATERIAL_MATERIAL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MATERIAL_MATERIAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMaterialMaterialLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MATERIAL_MATERIAL> Unlock(long id)
        {
            ApiResultObject<HIS_MATERIAL_MATERIAL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MATERIAL_MATERIAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMaterialMaterialLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMaterialMaterialTruncate(param).Truncate(id);
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
