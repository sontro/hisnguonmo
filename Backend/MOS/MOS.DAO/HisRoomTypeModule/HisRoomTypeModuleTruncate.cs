using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRoomTypeModule
{
    partial class HisRoomTypeModuleTruncate : EntityBase
    {
        public HisRoomTypeModuleTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_TYPE_MODULE>();
        }

        private BridgeDAO<HIS_ROOM_TYPE_MODULE> bridgeDAO;

        public bool Truncate(HIS_ROOM_TYPE_MODULE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ROOM_TYPE_MODULE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
