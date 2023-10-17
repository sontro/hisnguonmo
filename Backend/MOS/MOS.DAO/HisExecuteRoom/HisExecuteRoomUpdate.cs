using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExecuteRoom
{
    partial class HisExecuteRoomUpdate : EntityBase
    {
        public HisExecuteRoomUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_ROOM>();
        }

        private BridgeDAO<HIS_EXECUTE_ROOM> bridgeDAO;

        public bool Update(HIS_EXECUTE_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXECUTE_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
