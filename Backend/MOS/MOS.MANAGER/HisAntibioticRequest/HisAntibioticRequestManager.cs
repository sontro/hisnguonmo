using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticRequest
{
    public partial class HisAntibioticRequestManager : BusinessBase
    {
        public HisAntibioticRequestManager()
            : base()
        {

        }
        
        public HisAntibioticRequestManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ANTIBIOTIC_REQUEST>> Get(HisAntibioticRequestFilterQuery filter)
        {
            ApiResultObject<List<HIS_ANTIBIOTIC_REQUEST>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ANTIBIOTIC_REQUEST> resultData = null;
                if (valid)
                {
                    resultData = new HisAntibioticRequestGet(param).Get(filter);
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
        public ApiResultObject<HIS_ANTIBIOTIC_REQUEST> Create(HIS_ANTIBIOTIC_REQUEST data)
        {
            ApiResultObject<HIS_ANTIBIOTIC_REQUEST> result = new ApiResultObject<HIS_ANTIBIOTIC_REQUEST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIBIOTIC_REQUEST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAntibioticRequestCreate(param).Create(data);
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
        public ApiResultObject<HIS_ANTIBIOTIC_REQUEST> Update(HIS_ANTIBIOTIC_REQUEST data)
        {
            ApiResultObject<HIS_ANTIBIOTIC_REQUEST> result = new ApiResultObject<HIS_ANTIBIOTIC_REQUEST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIBIOTIC_REQUEST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAntibioticRequestUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ANTIBIOTIC_REQUEST> ChangeLock(long id)
        {
            ApiResultObject<HIS_ANTIBIOTIC_REQUEST> result = new ApiResultObject<HIS_ANTIBIOTIC_REQUEST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIBIOTIC_REQUEST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticRequestLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTIBIOTIC_REQUEST> Lock(long id)
        {
            ApiResultObject<HIS_ANTIBIOTIC_REQUEST> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIBIOTIC_REQUEST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticRequestLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTIBIOTIC_REQUEST> Unlock(long id)
        {
            ApiResultObject<HIS_ANTIBIOTIC_REQUEST> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIBIOTIC_REQUEST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticRequestLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAntibioticRequestTruncate(param).Truncate(id);
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
        public ApiResultObject<HisAntibioticRequestResultSDO> Request(HisAntibioticRequestSDO data)
        {
            ApiResultObject<HisAntibioticRequestResultSDO> result = new ApiResultObject<HisAntibioticRequestResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisAntibioticRequestResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticRequestSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<V_HIS_ANTIBIOTIC_REQUEST> Approve(HisAntibioticRequestApproveSDO data)
        {
            ApiResultObject<V_HIS_ANTIBIOTIC_REQUEST> result = new ApiResultObject<V_HIS_ANTIBIOTIC_REQUEST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_ANTIBIOTIC_REQUEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticRequestApprove(param).Run(data, ref resultData);
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
    }
}
