using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCashierRoom
{
    partial class HisCashierRoomCreate : EntityBase
    {
        public HisCashierRoomCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CASHIER_ROOM>();
        }

        private BridgeDAO<HIS_CASHIER_ROOM> bridgeDAO;

        public bool Create(HIS_CASHIER_ROOM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CASHIER_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
