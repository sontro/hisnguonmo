using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInvoice
{
    partial class HisInvoiceTruncate : EntityBase
    {
        public HisInvoiceTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE>();
        }

        private BridgeDAO<HIS_INVOICE> bridgeDAO;

        public bool Truncate(HIS_INVOICE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_INVOICE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
