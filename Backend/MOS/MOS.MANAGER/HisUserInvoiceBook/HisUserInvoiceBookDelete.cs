using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUserInvoiceBook
{
    partial class HisUserInvoiceBookDelete : BusinessBase
    {
        internal HisUserInvoiceBookDelete()
            : base()
        {

        }

        internal HisUserInvoiceBookDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_USER_INVOICE_BOOK data)
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
                    result = DAOWorker.HisUserInvoiceBookDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_USER_INVOICE_BOOK> listData)
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
                    result = DAOWorker.HisUserInvoiceBookDAO.DeleteList(listData);
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
