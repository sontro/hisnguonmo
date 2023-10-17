using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRoom
{
    partial class HisRoomUpdate : EntityBase
    {
        public HisRoomUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM>();
        }

        private BridgeDAO<HIS_ROOM> bridgeDAO;

        public bool Update(HIS_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
