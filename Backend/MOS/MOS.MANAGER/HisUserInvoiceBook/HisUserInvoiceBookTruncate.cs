using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUserInvoiceBook
{
    partial class HisUserInvoiceBookTruncate : BusinessBase
    {
        internal HisUserInvoiceBookTruncate()
            : base()
        {

        }

        internal HisUserInvoiceBookTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_USER_INVOICE_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserInvoiceBookCheck checker = new HisUserInvoiceBookCheck(param);
                valid = valid && IsNotNull(data);
                HIS_USER_INVOICE_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisUserInvoiceBookDAO.Truncate(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool TruncateList(List<HIS_USER_INVOICE_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUserInvoiceBookCheck checker = new HisUserInvoiceBookCheck(param);
                List<HIS_USER_INVOICE_BOOK> listRaw = new List<HIS_USER_INVOICE_BOOK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisUserInvoiceBookDAO.TruncateList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool TruncateByInvoiceBookId(long invoiceBookId)
        {
            bool result = true;
            List<HIS_USER_INVOICE_BOOK> listData = new HisUserInvoiceBookGet().GetByInvoiceBookId(invoiceBookId);
            if (IsNotNullOrEmpty(listData))
            {
                result = this.TruncateList(listData);
            }
            return result;
        }
    }
}
