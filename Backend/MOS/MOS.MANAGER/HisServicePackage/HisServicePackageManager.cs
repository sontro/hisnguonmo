using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServicePackage
{
    public partial class HisServicePackageManager : BusinessBase
    {
        public HisServicePackageManager()
            : base()
        {

        }
        
        public HisServicePackageManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERVICE_PACKAGE>> Get(HisServicePackageFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_PACKAGE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_PACKAGE> resultData = null;
                if (valid)
                {
                    resultData = new HisServicePackageGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_SERVICE_PACKAGE>> GetView(HisServicePackageViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_PACKAGE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_PACKAGE> resultData = null;
                if (valid)
                {
                    resultData = new HisServicePackageGet(param).GetView(filter);
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
        public ApiResultObject<HIS_SERVICE_PACKAGE> Create(HIS_SERVICE_PACKAGE data)
        {
            ApiResultObject<HIS_SERVICE_PACKAGE> result = new ApiResultObject<HIS_SERVICE_PACKAGE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_PACKAGE resultData = null;
                if (valid && new HisServicePackageCreate(param).Create(data))
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
        public ApiResultObject<HIS_SERVICE_PACKAGE> Update(HIS_SERVICE_PACKAGE data)
        {
            ApiResultObject<HIS_SERVICE_PACKAGE> result = new ApiResultObject<HIS_SERVICE_PACKAGE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_PACKAGE resultData = null;
                if (valid && new HisServicePackageUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SERVICE_PACKAGE> ChangeLock(HIS_SERVICE_PACKAGE data)
        {
            ApiResultObject<HIS_SERVICE_PACKAGE> result = new ApiResultObject<HIS_SERVICE_PACKAGE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_PACKAGE resultData = null;
                if (valid && new HisServicePackageLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_SERVICE_PACKAGE data)
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
                    resultData = new HisServicePackageTruncate(param).Truncate(data);
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
