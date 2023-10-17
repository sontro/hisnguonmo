using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCard
{
    partial class HisCardTruncate : EntityBase
    {
        public HisCardTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARD>();
        }

        private BridgeDAO<HIS_CARD> bridgeDAO;

        public bool Truncate(HIS_CARD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CARD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
