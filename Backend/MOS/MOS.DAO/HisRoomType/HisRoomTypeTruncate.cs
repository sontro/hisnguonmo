using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRoomType
{
    partial class HisRoomTypeTruncate : EntityBase
    {
        public HisRoomTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_TYPE>();
        }

        private BridgeDAO<HIS_ROOM_TYPE> bridgeDAO;

        public bool Truncate(HIS_ROOM_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ROOM_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
