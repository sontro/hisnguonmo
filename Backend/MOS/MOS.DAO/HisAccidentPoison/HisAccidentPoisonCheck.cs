using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAccidentPoison
{
    partial class HisAccidentPoisonCheck : EntityBase
    {
        public HisAccidentPoisonCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_POISON>();
        }

        private BridgeDAO<HIS_ACCIDENT_POISON> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
