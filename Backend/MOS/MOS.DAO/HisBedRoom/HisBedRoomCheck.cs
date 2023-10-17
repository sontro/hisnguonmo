using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisBedRoom
{
    partial class HisBedRoomCheck : EntityBase
    {
        public HisBedRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_ROOM>();
        }

        private BridgeDAO<HIS_BED_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
