using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAnticipateMety
{
    partial class HisAnticipateMetyCheck : EntityBase
    {
        public HisAnticipateMetyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE_METY>();
        }

        private BridgeDAO<HIS_ANTICIPATE_METY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
