using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestRoom
{
    partial class HisMestRoomTruncate : EntityBase
    {
        public HisMestRoomTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_ROOM>();
        }

        private BridgeDAO<HIS_MEST_ROOM> bridgeDAO;

        public bool Truncate(HIS_MEST_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEST_ROOM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
