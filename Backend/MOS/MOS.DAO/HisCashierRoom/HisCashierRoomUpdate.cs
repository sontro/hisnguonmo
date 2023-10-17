using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCashierRoom
{
    partial class HisCashierRoomUpdate : EntityBase
    {
        public HisCashierRoomUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CASHIER_ROOM>();
        }

        private BridgeDAO<HIS_CASHIER_ROOM> bridgeDAO;

        public bool Update(HIS_CASHIER_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CASHIER_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
