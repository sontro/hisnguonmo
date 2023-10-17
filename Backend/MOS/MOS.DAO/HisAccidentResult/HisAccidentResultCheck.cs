using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAccidentResult
{
    partial class HisAccidentResultCheck : EntityBase
    {
        public HisAccidentResultCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_RESULT>();
        }

        private BridgeDAO<HIS_ACCIDENT_RESULT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
