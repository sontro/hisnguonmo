using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentLocation
{
    partial class HisAccidentLocationTruncate : EntityBase
    {
        public HisAccidentLocationTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_LOCATION>();
        }

        private BridgeDAO<HIS_ACCIDENT_LOCATION> bridgeDAO;

        public bool Truncate(HIS_ACCIDENT_LOCATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ACCIDENT_LOCATION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
