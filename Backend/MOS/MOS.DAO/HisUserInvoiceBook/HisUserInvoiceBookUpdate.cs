using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUserInvoiceBook
{
    partial class HisUserInvoiceBookUpdate : EntityBase
    {
        public HisUserInvoiceBookUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_INVOICE_BOOK>();
        }

        private BridgeDAO<HIS_USER_INVOICE_BOOK> bridgeDAO;

        public bool Update(HIS_USER_INVOICE_BOOK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_USER_INVOICE_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
