using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMetyProduct
{
    partial class HisMetyProductTruncate : EntityBase
    {
        public HisMetyProductTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_METY_PRODUCT>();
        }

        private BridgeDAO<HIS_METY_PRODUCT> bridgeDAO;

        public bool Truncate(HIS_METY_PRODUCT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_METY_PRODUCT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
