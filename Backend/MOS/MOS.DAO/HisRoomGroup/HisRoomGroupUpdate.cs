using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRoomGroup
{
    partial class HisRoomGroupUpdate : EntityBase
    {
        public HisRoomGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_GROUP>();
        }

        private BridgeDAO<HIS_ROOM_GROUP> bridgeDAO;

        public bool Update(HIS_ROOM_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ROOM_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
