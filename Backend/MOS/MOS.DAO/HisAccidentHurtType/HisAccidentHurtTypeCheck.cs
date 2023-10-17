using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAccidentHurtType
{
    partial class HisAccidentHurtTypeCheck : EntityBase
    {
        public HisAccidentHurtTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_HURT_TYPE>();
        }

        private BridgeDAO<HIS_ACCIDENT_HURT_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
