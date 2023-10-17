using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInvoiceDetail
{
    partial class HisInvoiceDetailDelete : BusinessBase
    {
        internal HisInvoiceDetailDelete()
            : base()
        {

        }

        internal HisInvoiceDetailDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_INVOICE_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInvoiceDetailCheck checker = new HisInvoiceDetailCheck(param);
                valid = valid && IsNotNull(data);
                HIS_INVOICE_DETAIL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisInvoiceDetailDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_INVOICE_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInvoiceDetailCheck checker = new HisInvoiceDetailCheck(param);
                List<HIS_INVOICE_DETAIL> listRaw = new List<HIS_INVOICE_DETAIL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisInvoiceDetailDAO.DeleteList(listData);
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
