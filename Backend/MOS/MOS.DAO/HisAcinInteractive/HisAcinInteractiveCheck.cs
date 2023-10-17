using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAcinInteractive
{
    partial class HisAcinInteractiveCheck : EntityBase
    {
        public HisAcinInteractiveCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACIN_INTERACTIVE>();
        }

        private BridgeDAO<HIS_ACIN_INTERACTIVE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
