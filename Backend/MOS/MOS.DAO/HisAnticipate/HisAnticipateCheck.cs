using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAnticipate
{
    partial class HisAnticipateCheck : EntityBase
    {
        public HisAnticipateCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE>();
        }

        private BridgeDAO<HIS_ANTICIPATE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
