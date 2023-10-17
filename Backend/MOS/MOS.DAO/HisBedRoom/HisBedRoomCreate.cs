using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisBedRoom
{
    partial class HisBedRoomCreate : EntityBase
    {
        public HisBedRoomCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_ROOM>();
        }

        private BridgeDAO<HIS_BED_ROOM> bridgeDAO;

        public bool Create(HIS_BED_ROOM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BED_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
