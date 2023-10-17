using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRoom
{
    partial class HisRoomTruncate : EntityBase
    {
        public HisRoomTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM>();
        }

        private BridgeDAO<HIS_ROOM> bridgeDAO;

        public bool Truncate(HIS_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ROOM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
