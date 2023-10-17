using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisStation
{
    partial class HisStationCreate : EntityBase
    {
        public HisStationCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_STATION>();
        }

        private BridgeDAO<HIS_STATION> bridgeDAO;

        public bool Create(HIS_STATION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_STATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
