using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAccountBook
{
    partial class HisAccountBookCheck : EntityBase
    {
        public HisAccountBookCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCOUNT_BOOK>();
        }

        private BridgeDAO<HIS_ACCOUNT_BOOK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
