using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCarerCardBorrow
{
    partial class HisCarerCardBorrowTruncate : EntityBase
    {
        public HisCarerCardBorrowTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARER_CARD_BORROW>();
        }

        private BridgeDAO<HIS_CARER_CARD_BORROW> bridgeDAO;

        public bool Truncate(HIS_CARER_CARD_BORROW data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CARER_CARD_BORROW> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
