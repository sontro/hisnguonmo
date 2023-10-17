using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRoomTime
{
    partial class HisRoomTimeTruncate : EntityBase
    {
        public HisRoomTimeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_TIME>();
        }

        private BridgeDAO<HIS_ROOM_TIME> bridgeDAO;

        public bool Truncate(HIS_ROOM_TIME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ROOM_TIME> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
