using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisInvoicePrint
{
    partial class HisInvoicePrintCheck : EntityBase
    {
        public HisInvoicePrintCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE_PRINT>();
        }

        private BridgeDAO<HIS_INVOICE_PRINT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
