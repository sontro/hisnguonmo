using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCarerCardBorrow
{
    partial class HisCarerCardBorrowCreate : EntityBase
    {
        public HisCarerCardBorrowCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARER_CARD_BORROW>();
        }

        private BridgeDAO<HIS_CARER_CARD_BORROW> bridgeDAO;

        public bool Create(HIS_CARER_CARD_BORROW data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CARER_CARD_BORROW> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
