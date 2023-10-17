using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentLocation
{
    partial class HisAccidentLocationUpdate : EntityBase
    {
        public HisAccidentLocationUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_LOCATION>();
        }

        private BridgeDAO<HIS_ACCIDENT_LOCATION> bridgeDAO;

        public bool Update(HIS_ACCIDENT_LOCATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ACCIDENT_LOCATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
