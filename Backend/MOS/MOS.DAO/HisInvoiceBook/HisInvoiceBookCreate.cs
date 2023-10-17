using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisInvoiceBook
{
    partial class HisInvoiceBookCreate : EntityBase
    {
        public HisInvoiceBookCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE_BOOK>();
        }

        private BridgeDAO<HIS_INVOICE_BOOK> bridgeDAO;

        public bool Create(HIS_INVOICE_BOOK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_INVOICE_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
