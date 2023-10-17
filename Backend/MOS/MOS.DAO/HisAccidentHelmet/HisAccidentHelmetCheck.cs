using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAccidentHelmet
{
    partial class HisAccidentHelmetCheck : EntityBase
    {
        public HisAccidentHelmetCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_HELMET>();
        }

        private BridgeDAO<HIS_ACCIDENT_HELMET> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
