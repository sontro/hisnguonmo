using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRoomTime
{
    partial class HisRoomTimeUpdate : EntityBase
    {
        public HisRoomTimeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_TIME>();
        }

        private BridgeDAO<HIS_ROOM_TIME> bridgeDAO;

        public bool Update(HIS_ROOM_TIME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ROOM_TIME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
