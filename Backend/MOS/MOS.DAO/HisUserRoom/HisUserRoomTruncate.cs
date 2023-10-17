using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUserRoom
{
    partial class HisUserRoomTruncate : EntityBase
    {
        public HisUserRoomTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_ROOM>();
        }

        private BridgeDAO<HIS_USER_ROOM> bridgeDAO;

        public bool Truncate(HIS_USER_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_USER_ROOM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
