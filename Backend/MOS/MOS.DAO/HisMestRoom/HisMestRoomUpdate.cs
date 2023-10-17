using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestRoom
{
    partial class HisMestRoomUpdate : EntityBase
    {
        public HisMestRoomUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_ROOM>();
        }

        private BridgeDAO<HIS_MEST_ROOM> bridgeDAO;

        public bool Update(HIS_MEST_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
