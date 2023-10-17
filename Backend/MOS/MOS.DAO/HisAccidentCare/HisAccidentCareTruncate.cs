using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentCare
{
    partial class HisAccidentCareTruncate : EntityBase
    {
        public HisAccidentCareTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_CARE>();
        }

        private BridgeDAO<HIS_ACCIDENT_CARE> bridgeDAO;

        public bool Truncate(HIS_ACCIDENT_CARE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ACCIDENT_CARE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
