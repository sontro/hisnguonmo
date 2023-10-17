using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisInvoice
{
    partial class HisInvoiceCheck : EntityBase
    {
        public HisInvoiceCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE>();
        }

        private BridgeDAO<HIS_INVOICE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
