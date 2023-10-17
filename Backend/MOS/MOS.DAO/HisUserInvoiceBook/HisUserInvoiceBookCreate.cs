using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisUserInvoiceBook
{
    partial class HisUserInvoiceBookCreate : EntityBase
    {
        public HisUserInvoiceBookCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_INVOICE_BOOK>();
        }

        private BridgeDAO<HIS_USER_INVOICE_BOOK> bridgeDAO;

        public bool Create(HIS_USER_INVOICE_BOOK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_USER_INVOICE_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
