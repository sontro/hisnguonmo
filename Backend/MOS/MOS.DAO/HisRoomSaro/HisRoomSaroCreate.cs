using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRoomSaro
{
    partial class HisRoomSaroCreate : EntityBase
    {
        public HisRoomSaroCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_SARO>();
        }

        private BridgeDAO<HIS_ROOM_SARO> bridgeDAO;

        public bool Create(HIS_ROOM_SARO data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ROOM_SARO> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
