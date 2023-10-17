using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInvoicePrint
{
    partial class HisInvoicePrintUpdate : EntityBase
    {
        public HisInvoicePrintUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE_PRINT>();
        }

        private BridgeDAO<HIS_INVOICE_PRINT> bridgeDAO;

        public bool Update(HIS_INVOICE_PRINT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_INVOICE_PRINT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
