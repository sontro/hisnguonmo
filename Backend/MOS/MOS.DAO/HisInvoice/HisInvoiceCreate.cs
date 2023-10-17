using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisInvoice
{
    partial class HisInvoiceCreate : EntityBase
    {
        public HisInvoiceCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE>();
        }

        private BridgeDAO<HIS_INVOICE> bridgeDAO;

        public bool Create(HIS_INVOICE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_INVOICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
