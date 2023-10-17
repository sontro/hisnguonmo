using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCarerCard
{
    partial class HisCarerCardTruncate : EntityBase
    {
        public HisCarerCardTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARER_CARD>();
        }

        private BridgeDAO<HIS_CARER_CARD> bridgeDAO;

        public bool Truncate(HIS_CARER_CARD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CARER_CARD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
