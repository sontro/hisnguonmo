using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCashierRoom
{
    partial class HisCashierRoomTruncate : EntityBase
    {
        public HisCashierRoomTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CASHIER_ROOM>();
        }

        private BridgeDAO<HIS_CASHIER_ROOM> bridgeDAO;

        public bool Truncate(HIS_CASHIER_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CASHIER_ROOM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
