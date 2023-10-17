using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRoomTypeModule
{
    partial class HisRoomTypeModuleUpdate : EntityBase
    {
        public HisRoomTypeModuleUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM_TYPE_MODULE>();
        }

        private BridgeDAO<HIS_ROOM_TYPE_MODULE> bridgeDAO;

        public bool Update(HIS_ROOM_TYPE_MODULE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ROOM_TYPE_MODULE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
