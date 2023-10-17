using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisBedRoom
{
    partial class HisBedRoomUpdate : EntityBase
    {
        public HisBedRoomUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_ROOM>();
        }

        private BridgeDAO<HIS_BED_ROOM> bridgeDAO;

        public bool Update(HIS_BED_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BED_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
