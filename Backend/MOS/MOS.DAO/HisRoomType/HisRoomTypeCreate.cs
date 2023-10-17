using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRoomType
{
    partial class HisRoomTypeCreate : EntityBase
    {
        public HisRoomTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_TYPE>();
        }

        private BridgeDAO<HIS_ROOM_TYPE> bridgeDAO;

        public bool Create(HIS_ROOM_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ROOM_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
