using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.TDO;

namespace MOS.MANAGER.HisDrugIntervention
{
    public partial class HisDrugInterventionManager : BusinessBase
    {
        public HisDrugInterventionManager()
            : base()
        {

        }
        
        public HisDrugInterventionManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_DRUG_INTERVENTION>> Get(HisDrugInterventionFilterQuery filter)
        {
            ApiResultObject<List<HIS_DRUG_INTERVENTION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DRUG_INTERVENTION> resultData = null;
                if (valid)
                {
                    resultData = new HisDrugInterventionGet(param).Get(filter);
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
        public ApiResultObject<HIS_DRUG_INTERVENTION> Create(HIS_DRUG_INTERVENTION data)
        {
            ApiResultObject<HIS_DRUG_INTERVENTION> result = new ApiResultObject<HIS_DRUG_INTERVENTION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DRUG_INTERVENTION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDrugInterventionCreate(param).Create(data);
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
        public ApiResultObject<HIS_DRUG_INTERVENTION> Update(HIS_DRUG_INTERVENTION data)
        {
            ApiResultObject<HIS_DRUG_INTERVENTION> result = new ApiResultObject<HIS_DRUG_INTERVENTION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DRUG_INTERVENTION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDrugInterventionUpdate(param).Update(data);
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
        public ApiResultObject<HIS_DRUG_INTERVENTION> ChangeLock(long id)
        {
            ApiResultObject<HIS_DRUG_INTERVENTION> result = new ApiResultObject<HIS_DRUG_INTERVENTION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DRUG_INTERVENTION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDrugInterventionLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_DRUG_INTERVENTION> Lock(long id)
        {
            ApiResultObject<HIS_DRUG_INTERVENTION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DRUG_INTERVENTION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDrugInterventionLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_DRUG_INTERVENTION> Unlock(long id)
        {
            ApiResultObject<HIS_DRUG_INTERVENTION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DRUG_INTERVENTION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDrugInterventionLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisDrugInterventionTruncate(param).Truncate(id);
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
        public ApiResultObject<bool> CreateInfo(DrugInterventionInfoTDO data)
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
                    resultData = new HisDrugInterventionCreate(param).CreateInfo(data);
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
