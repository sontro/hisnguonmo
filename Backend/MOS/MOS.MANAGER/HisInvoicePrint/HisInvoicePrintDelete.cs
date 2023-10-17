using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInvoicePrint
{
    partial class HisInvoicePrintDelete : BusinessBase
    {
        internal HisInvoicePrintDelete()
            : base()
        {

        }

        internal HisInvoicePrintDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_INVOICE_PRINT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInvoicePrintCheck checker = new HisInvoicePrintCheck(param);
                valid = valid && IsNotNull(data);
                HIS_INVOICE_PRINT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisInvoicePrintDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_INVOICE_PRINT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInvoicePrintCheck checker = new HisInvoicePrintCheck(param);
                List<HIS_INVOICE_PRINT> listRaw = new List<HIS_INVOICE_PRINT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisInvoicePrintDAO.DeleteList(listData);
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
