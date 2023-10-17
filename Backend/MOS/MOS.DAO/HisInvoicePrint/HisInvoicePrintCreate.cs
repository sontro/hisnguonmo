using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisInvoicePrint
{
    partial class HisInvoicePrintCreate : EntityBase
    {
        public HisInvoicePrintCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE_PRINT>();
        }

        private BridgeDAO<HIS_INVOICE_PRINT> bridgeDAO;

        public bool Create(HIS_INVOICE_PRINT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_INVOICE_PRINT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
