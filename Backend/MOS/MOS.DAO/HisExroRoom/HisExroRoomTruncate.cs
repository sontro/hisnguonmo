using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExroRoom
{
    partial class HisExroRoomTruncate : EntityBase
    {
        public HisExroRoomTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXRO_ROOM>();
        }

        private BridgeDAO<HIS_EXRO_ROOM> bridgeDAO;

        public bool Truncate(HIS_EXRO_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXRO_ROOM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
