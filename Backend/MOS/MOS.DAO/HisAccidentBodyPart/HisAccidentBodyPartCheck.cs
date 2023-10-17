using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAccidentBodyPart
{
    partial class HisAccidentBodyPartCheck : EntityBase
    {
        public HisAccidentBodyPartCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_BODY_PART>();
        }

        private BridgeDAO<HIS_ACCIDENT_BODY_PART> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
