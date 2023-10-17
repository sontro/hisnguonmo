using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInvoicePrint
{
    public partial class HisInvoicePrintManager : BusinessBase
    {
        public HisInvoicePrintManager()
            : base()
        {

        }
        
        public HisInvoicePrintManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_INVOICE_PRINT>> Get(HisInvoicePrintFilterQuery filter)
        {
            ApiResultObject<List<HIS_INVOICE_PRINT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_INVOICE_PRINT> resultData = null;
                if (valid)
                {
                    resultData = new HisInvoicePrintGet(param).Get(filter);
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
        public ApiResultObject<HIS_INVOICE_PRINT> Create(HIS_INVOICE_PRINT data)
        {
            ApiResultObject<HIS_INVOICE_PRINT> result = new ApiResultObject<HIS_INVOICE_PRINT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INVOICE_PRINT resultData = null;
                if (valid && new HisInvoicePrintCreate(param).Create(data))
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
        public ApiResultObject<HIS_INVOICE_PRINT> Update(HIS_INVOICE_PRINT data)
        {
            ApiResultObject<HIS_INVOICE_PRINT> result = new ApiResultObject<HIS_INVOICE_PRINT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INVOICE_PRINT resultData = null;
                if (valid && new HisInvoicePrintUpdate(param).Update(data))
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
        public ApiResultObject<HIS_INVOICE_PRINT> ChangeLock(HIS_INVOICE_PRINT data)
        {
            ApiResultObject<HIS_INVOICE_PRINT> result = new ApiResultObject<HIS_INVOICE_PRINT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INVOICE_PRINT resultData = null;
                if (valid && new HisInvoicePrintLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_INVOICE_PRINT data)
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
                    resultData = new HisInvoicePrintTruncate(param).Truncate(data);
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
