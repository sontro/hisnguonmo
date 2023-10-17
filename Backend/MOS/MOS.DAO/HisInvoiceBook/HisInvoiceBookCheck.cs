using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisInvoiceBook
{
    partial class HisInvoiceBookCheck : EntityBase
    {
        public HisInvoiceBookCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE_BOOK>();
        }

        private BridgeDAO<HIS_INVOICE_BOOK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
