using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUserRoom
{
    partial class HisUserRoomUpdate : EntityBase
    {
        public HisUserRoomUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_ROOM>();
        }

        private BridgeDAO<HIS_USER_ROOM> bridgeDAO;

        public bool Update(HIS_USER_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_USER_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
