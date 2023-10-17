using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInvoiceDetail
{
    partial class HisInvoiceDetailTruncate : EntityBase
    {
        public HisInvoiceDetailTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE_DETAIL>();
        }

        private BridgeDAO<HIS_INVOICE_DETAIL> bridgeDAO;

        public bool Truncate(HIS_INVOICE_DETAIL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_INVOICE_DETAIL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
