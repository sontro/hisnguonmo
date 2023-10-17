using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisPackage.UpdateDetail;

namespace MOS.MANAGER.HisPackage
{
    public partial class HisPackageManager : BusinessBase
    {
        public HisPackageManager()
            : base()
        {

        }
        
        public HisPackageManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PACKAGE>> Get(HisPackageFilterQuery filter)
        {
            ApiResultObject<List<HIS_PACKAGE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PACKAGE> resultData = null;
                if (valid)
                {
                    resultData = new HisPackageGet(param).Get(filter);
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
        public ApiResultObject<HIS_PACKAGE> Create(HIS_PACKAGE data)
        {
            ApiResultObject<HIS_PACKAGE> result = new ApiResultObject<HIS_PACKAGE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PACKAGE resultData = null;
                if (valid && new HisPackageCreate(param).Create(data))
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
        public ApiResultObject<HIS_PACKAGE> Update(HIS_PACKAGE data)
        {
            ApiResultObject<HIS_PACKAGE> result = new ApiResultObject<HIS_PACKAGE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PACKAGE resultData = null;
                if (valid && new HisPackageUpdate(param).Update(data))
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
        public ApiResultObject<bool> UpdateDetail(HisPackageSDO data)
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
                    resultData = new HisPackageUpdateDetail(param).Run(data);
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
        public ApiResultObject<HIS_PACKAGE> ChangeLock(long id)
        {
            ApiResultObject<HIS_PACKAGE> result = new ApiResultObject<HIS_PACKAGE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PACKAGE resultData = null;
                if (valid)
                {
                    new HisPackageLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PACKAGE> Lock(long id)
        {
            ApiResultObject<HIS_PACKAGE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PACKAGE resultData = null;
                if (valid)
                {
                    new HisPackageLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PACKAGE> Unlock(long id)
        {
            ApiResultObject<HIS_PACKAGE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PACKAGE resultData = null;
                if (valid)
                {
                    new HisPackageLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPackageTruncate(param).Truncate(id);
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
