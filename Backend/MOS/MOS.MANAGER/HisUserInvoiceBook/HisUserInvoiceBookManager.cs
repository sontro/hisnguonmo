using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserInvoiceBook
{
    public partial class HisUserInvoiceBookManager : BusinessBase
    {
        public HisUserInvoiceBookManager()
            : base()
        {

        }
        
        public HisUserInvoiceBookManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_USER_INVOICE_BOOK>> Get(HisUserInvoiceBookFilterQuery filter)
        {
            ApiResultObject<List<HIS_USER_INVOICE_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_USER_INVOICE_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisUserInvoiceBookGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_USER_INVOICE_BOOK>> GetView(HisUserInvoiceBookViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_USER_INVOICE_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_USER_INVOICE_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisUserInvoiceBookGet(param).GetView(filter);
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
        public ApiResultObject<List<HIS_USER_INVOICE_BOOK>> Create(HisUserInvoiceBookSDO data)
        {
            ApiResultObject<List<HIS_USER_INVOICE_BOOK>> result = new ApiResultObject<List<HIS_USER_INVOICE_BOOK>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_USER_INVOICE_BOOK> resultData = null;
                if (valid)
                {
                    new HisUserInvoiceBookCreate(param).Create(data, ref resultData);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_USER_INVOICE_BOOK> Update(HIS_USER_INVOICE_BOOK data)
        {
            ApiResultObject<HIS_USER_INVOICE_BOOK> result = new ApiResultObject<HIS_USER_INVOICE_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_USER_INVOICE_BOOK resultData = null;
                if (valid && new HisUserInvoiceBookUpdate(param).Update(data))
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
        public ApiResultObject<HIS_USER_INVOICE_BOOK> ChangeLock(HIS_USER_INVOICE_BOOK data)
        {
            ApiResultObject<HIS_USER_INVOICE_BOOK> result = new ApiResultObject<HIS_USER_INVOICE_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_USER_INVOICE_BOOK resultData = null;
                if (valid && new HisUserInvoiceBookLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_USER_INVOICE_BOOK data)
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
                    resultData = new HisUserInvoiceBookTruncate(param).Truncate(data);
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
