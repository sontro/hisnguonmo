using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisAccidentCare
{
    partial class HisAccidentCareCheck : EntityBase
    {
        public HisAccidentCareCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_CARE>();
        }

        private BridgeDAO<HIS_ACCIDENT_CARE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
