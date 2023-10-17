using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRoomSaro
{
    partial class HisRoomSaroTruncate : EntityBase
    {
        public HisRoomSaroTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_SARO>();
        }

        private BridgeDAO<HIS_ROOM_SARO> bridgeDAO;

        public bool Truncate(HIS_ROOM_SARO data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ROOM_SARO> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
