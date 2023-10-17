using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccountBook
{
    partial class HisAccountBookCreate : EntityBase
    {
        public HisAccountBookCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCOUNT_BOOK>();
        }

        private BridgeDAO<HIS_ACCOUNT_BOOK> bridgeDAO;

        public bool Create(HIS_ACCOUNT_BOOK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ACCOUNT_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
