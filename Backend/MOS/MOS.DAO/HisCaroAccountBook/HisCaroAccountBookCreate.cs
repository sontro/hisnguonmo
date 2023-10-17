using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCaroAccountBook
{
    partial class HisCaroAccountBookCreate : EntityBase
    {
        public HisCaroAccountBookCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARO_ACCOUNT_BOOK>();
        }

        private BridgeDAO<HIS_CARO_ACCOUNT_BOOK> bridgeDAO;

        public bool Create(HIS_CARO_ACCOUNT_BOOK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CARO_ACCOUNT_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
