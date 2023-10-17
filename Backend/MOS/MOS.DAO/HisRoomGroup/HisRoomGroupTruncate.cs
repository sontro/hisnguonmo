using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRoomGroup
{
    partial class HisRoomGroupTruncate : EntityBase
    {
        public HisRoomGroupTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_GROUP>();
        }

        private BridgeDAO<HIS_ROOM_GROUP> bridgeDAO;

        public bool Truncate(HIS_ROOM_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ROOM_GROUP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
