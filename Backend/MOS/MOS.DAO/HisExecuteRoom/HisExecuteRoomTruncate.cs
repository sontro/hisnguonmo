using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExecuteRoom
{
    partial class HisExecuteRoomTruncate : EntityBase
    {
        public HisExecuteRoomTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_ROOM>();
        }

        private BridgeDAO<HIS_EXECUTE_ROOM> bridgeDAO;

        public bool Truncate(HIS_EXECUTE_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXECUTE_ROOM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
