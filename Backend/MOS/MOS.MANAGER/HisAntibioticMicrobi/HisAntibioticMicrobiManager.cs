using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticMicrobi
{
    public partial class HisAntibioticMicrobiManager : BusinessBase
    {
        public HisAntibioticMicrobiManager()
            : base()
        {

        }
        
        public HisAntibioticMicrobiManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ANTIBIOTIC_MICROBI>> Get(HisAntibioticMicrobiFilterQuery filter)
        {
            ApiResultObject<List<HIS_ANTIBIOTIC_MICROBI>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ANTIBIOTIC_MICROBI> resultData = null;
                if (valid)
                {
                    resultData = new HisAntibioticMicrobiGet(param).Get(filter);
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
        public ApiResultObject<HIS_ANTIBIOTIC_MICROBI> Create(HIS_ANTIBIOTIC_MICROBI data)
        {
            ApiResultObject<HIS_ANTIBIOTIC_MICROBI> result = new ApiResultObject<HIS_ANTIBIOTIC_MICROBI>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIBIOTIC_MICROBI resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAntibioticMicrobiCreate(param).Create(data);
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
        public ApiResultObject<HIS_ANTIBIOTIC_MICROBI> Update(HIS_ANTIBIOTIC_MICROBI data)
        {
            ApiResultObject<HIS_ANTIBIOTIC_MICROBI> result = new ApiResultObject<HIS_ANTIBIOTIC_MICROBI>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIBIOTIC_MICROBI resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAntibioticMicrobiUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ANTIBIOTIC_MICROBI> ChangeLock(long id)
        {
            ApiResultObject<HIS_ANTIBIOTIC_MICROBI> result = new ApiResultObject<HIS_ANTIBIOTIC_MICROBI>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIBIOTIC_MICROBI resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticMicrobiLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTIBIOTIC_MICROBI> Lock(long id)
        {
            ApiResultObject<HIS_ANTIBIOTIC_MICROBI> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIBIOTIC_MICROBI resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticMicrobiLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTIBIOTIC_MICROBI> Unlock(long id)
        {
            ApiResultObject<HIS_ANTIBIOTIC_MICROBI> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIBIOTIC_MICROBI resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticMicrobiLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAntibioticMicrobiTruncate(param).Truncate(id);
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
