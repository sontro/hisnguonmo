using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRoomSaro
{
    partial class HisRoomSaroUpdate : EntityBase
    {
        public HisRoomSaroUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_SARO>();
        }

        private BridgeDAO<HIS_ROOM_SARO> bridgeDAO;

        public bool Update(HIS_ROOM_SARO data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ROOM_SARO> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
