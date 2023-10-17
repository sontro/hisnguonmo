using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiForm
{
    public partial class HisTranPatiFormManager : BusinessBase
    {
        public HisTranPatiFormManager()
            : base()
        {

        }
        
        public HisTranPatiFormManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TRAN_PATI_FORM>> Get(HisTranPatiFormFilterQuery filter)
        {
            ApiResultObject<List<HIS_TRAN_PATI_FORM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TRAN_PATI_FORM> resultData = null;
                if (valid)
                {
                    resultData = new HisTranPatiFormGet(param).Get(filter);
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
        public ApiResultObject<HIS_TRAN_PATI_FORM> Create(HIS_TRAN_PATI_FORM data)
        {
            ApiResultObject<HIS_TRAN_PATI_FORM> result = new ApiResultObject<HIS_TRAN_PATI_FORM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRAN_PATI_FORM resultData = null;
                if (valid && new HisTranPatiFormCreate(param).Create(data))
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
        public ApiResultObject<HIS_TRAN_PATI_FORM> Update(HIS_TRAN_PATI_FORM data)
        {
            ApiResultObject<HIS_TRAN_PATI_FORM> result = new ApiResultObject<HIS_TRAN_PATI_FORM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRAN_PATI_FORM resultData = null;
                if (valid && new HisTranPatiFormUpdate(param).Update(data))
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
        public ApiResultObject<HIS_TRAN_PATI_FORM> ChangeLock(HIS_TRAN_PATI_FORM data)
        {
            ApiResultObject<HIS_TRAN_PATI_FORM> result = new ApiResultObject<HIS_TRAN_PATI_FORM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRAN_PATI_FORM resultData = null;
                if (valid && new HisTranPatiFormLock(param).ChangeLock(data.ID))
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
        public ApiResultObject<bool> Delete(HIS_TRAN_PATI_FORM data)
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
                    resultData = new HisTranPatiFormTruncate(param).Truncate(data);
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
