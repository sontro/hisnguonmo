using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentLocation
{
    partial class HisAccidentLocationCreate : EntityBase
    {
        public HisAccidentLocationCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_LOCATION>();
        }

        private BridgeDAO<HIS_ACCIDENT_LOCATION> bridgeDAO;

        public bool Create(HIS_ACCIDENT_LOCATION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ACCIDENT_LOCATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
