using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAnticipateMaty
{
    partial class HisAnticipateMatyCheck : EntityBase
    {
        public HisAnticipateMatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE_MATY>();
        }

        private BridgeDAO<HIS_ANTICIPATE_MATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
