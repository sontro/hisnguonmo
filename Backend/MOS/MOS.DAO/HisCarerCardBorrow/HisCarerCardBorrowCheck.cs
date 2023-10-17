using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCarerCardBorrow
{
    partial class HisCarerCardBorrowCheck : EntityBase
    {
        public HisCarerCardBorrowCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARER_CARD_BORROW>();
        }

        private BridgeDAO<HIS_CARER_CARD_BORROW> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
