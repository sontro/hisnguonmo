using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEinvoiceType
{
    partial class HisEinvoiceTypeTruncate : EntityBase
    {
        public HisEinvoiceTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EINVOICE_TYPE>();
        }

        private BridgeDAO<HIS_EINVOICE_TYPE> bridgeDAO;

        public bool Truncate(HIS_EINVOICE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EINVOICE_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
