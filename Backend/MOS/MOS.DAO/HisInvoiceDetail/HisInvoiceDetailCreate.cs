using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisInvoiceDetail
{
    partial class HisInvoiceDetailCreate : EntityBase
    {
        public HisInvoiceDetailCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE_DETAIL>();
        }

        private BridgeDAO<HIS_INVOICE_DETAIL> bridgeDAO;

        public bool Create(HIS_INVOICE_DETAIL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_INVOICE_DETAIL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
