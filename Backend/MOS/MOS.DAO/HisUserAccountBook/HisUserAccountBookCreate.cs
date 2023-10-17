using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisUserAccountBook
{
    partial class HisUserAccountBookCreate : EntityBase
    {
        public HisUserAccountBookCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_ACCOUNT_BOOK>();
        }

        private BridgeDAO<HIS_USER_ACCOUNT_BOOK> bridgeDAO;

        public bool Create(HIS_USER_ACCOUNT_BOOK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_USER_ACCOUNT_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
