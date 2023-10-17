using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInvoice
{
    partial class HisInvoiceUpdate : EntityBase
    {
        public HisInvoiceUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INVOICE>();
        }

        private BridgeDAO<HIS_INVOICE> bridgeDAO;

        public bool Update(HIS_INVOICE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_INVOICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
