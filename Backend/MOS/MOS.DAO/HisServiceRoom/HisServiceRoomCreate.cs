using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceRoom
{
    partial class HisServiceRoomCreate : EntityBase
    {
        public HisServiceRoomCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_ROOM>();
        }

        private BridgeDAO<HIS_SERVICE_ROOM> bridgeDAO;

        public bool Create(HIS_SERVICE_ROOM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
