using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUserInvoiceBook
{
    partial class HisUserInvoiceBookTruncate : EntityBase
    {
        public HisUserInvoiceBookTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_INVOICE_BOOK>();
        }

        private BridgeDAO<HIS_USER_INVOICE_BOOK> bridgeDAO;

        public bool Truncate(HIS_USER_INVOICE_BOOK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_USER_INVOICE_BOOK> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
