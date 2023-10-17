using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInvoiceDetail
{
    partial class HisInvoiceDetailTruncate : BusinessBase
    {
        internal HisInvoiceDetailTruncate()
            : base()
        {

        }

        internal HisInvoiceDetailTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_INVOICE_DETAIL data)
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
                    result = DAOWorker.HisInvoiceDetailDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_INVOICE_DETAIL> listData)
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
                    result = DAOWorker.HisInvoiceDetailDAO.TruncateList(listData);
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
