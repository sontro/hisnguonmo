using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisInvoice;
using MOS.MANAGER.HisUserInvoiceBook;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInvoiceBook
{
    partial class HisInvoiceBookTruncate : BusinessBase
    {
        internal HisInvoiceBookTruncate()
            : base()
        {

        }

        internal HisInvoiceBookTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_INVOICE_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInvoiceBookCheck checker = new HisInvoiceBookCheck(param);
                valid = valid && IsNotNull(data);
                HIS_INVOICE_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    if (!new HisUserInvoiceBookTruncate(param).TruncateByInvoiceBookId(data.ID))
                    {
                        throw new Exception("Xoa du lieu HIS_USER_INVOICE_BOOK that bai. Ket thuc nghiep vu.");
                    }
                    result = DAOWorker.HisInvoiceBookDAO.Truncate(data);
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
    }
}
