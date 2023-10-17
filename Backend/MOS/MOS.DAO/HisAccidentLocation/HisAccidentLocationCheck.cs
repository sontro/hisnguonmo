using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAccidentLocation
{
    partial class HisAccidentLocationCheck : EntityBase
    {
        public HisAccidentLocationCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_LOCATION>();
        }

        private BridgeDAO<HIS_ACCIDENT_LOCATION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
