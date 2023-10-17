using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRoomGroup
{
    partial class HisRoomGroupCreate : EntityBase
    {
        public HisRoomGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_GROUP>();
        }

        private BridgeDAO<HIS_ROOM_GROUP> bridgeDAO;

        public bool Create(HIS_ROOM_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ROOM_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
