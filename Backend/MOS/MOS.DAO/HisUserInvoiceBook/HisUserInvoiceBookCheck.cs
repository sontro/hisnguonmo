using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisUserInvoiceBook
{
    partial class HisUserInvoiceBookCheck : EntityBase
    {
        public HisUserInvoiceBookCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_INVOICE_BOOK>();
        }

        private BridgeDAO<HIS_USER_INVOICE_BOOK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
