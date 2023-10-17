using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAccidentHurt
{
    partial class HisAccidentHurtCheck : EntityBase
    {
        public HisAccidentHurtCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_HURT>();
        }

        private BridgeDAO<HIS_ACCIDENT_HURT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
