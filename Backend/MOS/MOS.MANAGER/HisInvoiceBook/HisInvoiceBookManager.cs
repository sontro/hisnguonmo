using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInvoiceBook
{
    public partial class HisInvoiceBookManager : BusinessBase
    {
        public HisInvoiceBookManager()
            : base()
        {

        }
        
        public HisInvoiceBookManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_INVOICE_BOOK>> Get(HisInvoiceBookFilterQuery filter)
        {
            ApiResultObject<List<HIS_INVOICE_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_INVOICE_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisInvoiceBookGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_INVOICE_BOOK>> GetView(HisInvoiceBookViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_INVOICE_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_INVOICE_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisInvoiceBookGet(param).GetView(filter);
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
        public ApiResultObject<HIS_INVOICE_BOOK> Create(HIS_INVOICE_BOOK data)
        {
            ApiResultObject<HIS_INVOICE_BOOK> result = new ApiResultObject<HIS_INVOICE_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INVOICE_BOOK resultData = null;
                if (valid && new HisInvoiceBookCreate(param).Create(data))
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
        public ApiResultObject<HIS_INVOICE_BOOK> Update(HIS_INVOICE_BOOK data)
        {
            ApiResultObject<HIS_INVOICE_BOOK> result = new ApiResultObject<HIS_INVOICE_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INVOICE_BOOK resultData = null;
                if (valid && new HisInvoiceBookUpdate(param).Update(data))
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
        public ApiResultObject<HIS_INVOICE_BOOK> ChangeLock(HIS_INVOICE_BOOK data)
        {
            ApiResultObject<HIS_INVOICE_BOOK> result = new ApiResultObject<HIS_INVOICE_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INVOICE_BOOK resultData = null;
                if (valid && new HisInvoiceBookLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_INVOICE_BOOK data)
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
                    resultData = new HisInvoiceBookTruncate(param).Truncate(data);
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
