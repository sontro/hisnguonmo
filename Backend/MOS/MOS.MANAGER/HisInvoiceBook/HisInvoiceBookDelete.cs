using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInvoiceBook
{
    partial class HisInvoiceBookDelete : BusinessBase
    {
        internal HisInvoiceBookDelete()
            : base()
        {

        }

        internal HisInvoiceBookDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_INVOICE_BOOK data)
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
                if (valid)
                {
                    result = DAOWorker.HisInvoiceBookDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_INVOICE_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInvoiceBookCheck checker = new HisInvoiceBookCheck(param);
                List<HIS_INVOICE_BOOK> listRaw = new List<HIS_INVOICE_BOOK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisInvoiceBookDAO.DeleteList(listData);
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
