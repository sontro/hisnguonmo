using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInvoiceDetail
{
    partial class HisInvoiceDetailUpdate : EntityBase
    {
        public HisInvoiceDetailUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE_DETAIL>();
        }

        private BridgeDAO<HIS_INVOICE_DETAIL> bridgeDAO;

        public bool Update(HIS_INVOICE_DETAIL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_INVOICE_DETAIL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
