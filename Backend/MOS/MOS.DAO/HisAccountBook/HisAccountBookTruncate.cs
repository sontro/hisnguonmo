using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccountBook
{
    partial class HisAccountBookTruncate : EntityBase
    {
        public HisAccountBookTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCOUNT_BOOK>();
        }

        private BridgeDAO<HIS_ACCOUNT_BOOK> bridgeDAO;

        public bool Truncate(HIS_ACCOUNT_BOOK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ACCOUNT_BOOK> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
