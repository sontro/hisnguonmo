using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisImpMestPropose.Create;
using MOS.MANAGER.HisImpMestPropose.Update;
using MOS.MANAGER.HisImpMestPropose.Delete;

namespace MOS.MANAGER.HisImpMestPropose
{
    public partial class HisImpMestProposeManager : BusinessBase
    {
        public HisImpMestProposeManager()
            : base()
        {

        }
        
        public HisImpMestProposeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_IMP_MEST_PROPOSE>> Get(HisImpMestProposeFilterQuery filter)
        {
            ApiResultObject<List<HIS_IMP_MEST_PROPOSE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_IMP_MEST_PROPOSE> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestProposeGet(param).Get(filter);
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
        public ApiResultObject<HisImpMestProposeResultSDO> Create(HisImpMestProposeSDO data)
        {
            ApiResultObject<HisImpMestProposeResultSDO> result = new ApiResultObject<HisImpMestProposeResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestProposeResultSDO resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisImpMestProposeCreateSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<HisImpMestProposeResultSDO> Update(HisImpMestProposeSDO data)
        {
            ApiResultObject<HisImpMestProposeResultSDO> result = new ApiResultObject<HisImpMestProposeResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestProposeResultSDO resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisImpMestProposeUpdateSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_IMP_MEST_PROPOSE> ChangeLock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_PROPOSE> result = new ApiResultObject<HIS_IMP_MEST_PROPOSE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_PROPOSE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisImpMestProposeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_IMP_MEST_PROPOSE> Lock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_PROPOSE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_PROPOSE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisImpMestProposeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_IMP_MEST_PROPOSE> Unlock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_PROPOSE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_PROPOSE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisImpMestProposeLock(param).Unlock(id, ref resultData);
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
        public ApiResultObject<bool> Delete(HisImpMestProposeDeleteSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisImpMestProposeTruncateSdo(param).Run(data);
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
