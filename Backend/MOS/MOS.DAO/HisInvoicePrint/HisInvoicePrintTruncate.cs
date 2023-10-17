using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInvoicePrint
{
    partial class HisInvoicePrintTruncate : EntityBase
    {
        public HisInvoicePrintTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE_PRINT>();
        }

        private BridgeDAO<HIS_INVOICE_PRINT> bridgeDAO;

        public bool Truncate(HIS_INVOICE_PRINT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_INVOICE_PRINT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
