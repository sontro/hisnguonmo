using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisInvoiceDetail
{
    partial class HisInvoiceDetailCheck : EntityBase
    {
        public HisInvoiceDetailCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE_DETAIL>();
        }

        private BridgeDAO<HIS_INVOICE_DETAIL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
