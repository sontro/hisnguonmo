using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInvoiceDetail
{
    public partial class HisInvoiceDetailManager : BusinessBase
    {
        public HisInvoiceDetailManager()
            : base()
        {

        }
        
        public HisInvoiceDetailManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_INVOICE_DETAIL>> Get(HisInvoiceDetailFilterQuery filter)
        {
            ApiResultObject<List<HIS_INVOICE_DETAIL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_INVOICE_DETAIL> resultData = null;
                if (valid)
                {
                    resultData = new HisInvoiceDetailGet(param).Get(filter);
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
        public ApiResultObject<HIS_INVOICE_DETAIL> ChangeLock(HIS_INVOICE_DETAIL data)
        {
            ApiResultObject<HIS_INVOICE_DETAIL> result = new ApiResultObject<HIS_INVOICE_DETAIL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INVOICE_DETAIL resultData = null;
                if (valid && new HisInvoiceDetailLock(param).ChangeLock(data))
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
    }
}
