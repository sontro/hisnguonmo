using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccountBook
{
    partial class HisAccountBookUpdate : EntityBase
    {
        public HisAccountBookUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCOUNT_BOOK>();
        }

        private BridgeDAO<HIS_ACCOUNT_BOOK> bridgeDAO;

        public bool Update(HIS_ACCOUNT_BOOK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ACCOUNT_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
