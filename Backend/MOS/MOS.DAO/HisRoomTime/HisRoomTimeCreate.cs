using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRoomTime
{
    partial class HisRoomTimeCreate : EntityBase
    {
        public HisRoomTimeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_TIME>();
        }

        private BridgeDAO<HIS_ROOM_TIME> bridgeDAO;

        public bool Create(HIS_ROOM_TIME data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ROOM_TIME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
