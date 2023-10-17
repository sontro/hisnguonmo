using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiReason
{
    public partial class HisTranPatiReasonManager : BusinessBase
    {
        public HisTranPatiReasonManager()
            : base()
        {

        }
        
        public HisTranPatiReasonManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TRAN_PATI_REASON>> Get(HisTranPatiReasonFilterQuery filter)
        {
            ApiResultObject<List<HIS_TRAN_PATI_REASON>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TRAN_PATI_REASON> resultData = null;
                if (valid)
                {
                    resultData = new HisTranPatiReasonGet(param).Get(filter);
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
        public ApiResultObject<HIS_TRAN_PATI_REASON> Create(HIS_TRAN_PATI_REASON data)
        {
            ApiResultObject<HIS_TRAN_PATI_REASON> result = new ApiResultObject<HIS_TRAN_PATI_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRAN_PATI_REASON resultData = null;
                if (valid && new HisTranPatiReasonCreate(param).Create(data))
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
        public ApiResultObject<HIS_TRAN_PATI_REASON> Update(HIS_TRAN_PATI_REASON data)
        {
            ApiResultObject<HIS_TRAN_PATI_REASON> result = new ApiResultObject<HIS_TRAN_PATI_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRAN_PATI_REASON resultData = null;
                if (valid && new HisTranPatiReasonUpdate(param).Update(data))
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
        public ApiResultObject<HIS_TRAN_PATI_REASON> ChangeLock(HIS_TRAN_PATI_REASON data)
        {
            ApiResultObject<HIS_TRAN_PATI_REASON> result = new ApiResultObject<HIS_TRAN_PATI_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRAN_PATI_REASON resultData = null;
                if (valid && new HisTranPatiReasonLock(param).ChangeLock(data.ID))
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
        public ApiResultObject<bool> Delete(HIS_TRAN_PATI_REASON data)
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
                    resultData = new HisTranPatiReasonTruncate(param).Truncate(data);
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
