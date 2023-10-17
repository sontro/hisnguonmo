using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCarerCardBorrow
{
    partial class HisCarerCardBorrowUpdate : EntityBase
    {
        public HisCarerCardBorrowUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARER_CARD_BORROW>();
        }

        private BridgeDAO<HIS_CARER_CARD_BORROW> bridgeDAO;

        public bool Update(HIS_CARER_CARD_BORROW data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CARER_CARD_BORROW> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
