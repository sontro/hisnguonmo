using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRoomTypeModule
{
    partial class HisRoomTypeModuleCreate : EntityBase
    {
        public HisRoomTypeModuleCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_TYPE_MODULE>();
        }

        private BridgeDAO<HIS_ROOM_TYPE_MODULE> bridgeDAO;

        public bool Create(HIS_ROOM_TYPE_MODULE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ROOM_TYPE_MODULE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
