using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAwareness
{
    partial class HisAwarenessCheck : EntityBase
    {
        public HisAwarenessCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_AWARENESS>();
        }

        private BridgeDAO<HIS_AWARENESS> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
