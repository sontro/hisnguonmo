using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInvoiceBook
{
    partial class HisInvoiceBookUpdate : EntityBase
    {
        public HisInvoiceBookUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE_BOOK>();
        }

        private BridgeDAO<HIS_INVOICE_BOOK> bridgeDAO;

        public bool Update(HIS_INVOICE_BOOK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_INVOICE_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
