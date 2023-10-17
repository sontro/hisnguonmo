using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEinvoiceType
{
    partial class HisEinvoiceTypeUpdate : EntityBase
    {
        public HisEinvoiceTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EINVOICE_TYPE>();
        }

        private BridgeDAO<HIS_EINVOICE_TYPE> bridgeDAO;

        public bool Update(HIS_EINVOICE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EINVOICE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
