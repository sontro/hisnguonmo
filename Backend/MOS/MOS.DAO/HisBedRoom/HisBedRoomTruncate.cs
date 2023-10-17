using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisBedRoom
{
    partial class HisBedRoomTruncate : EntityBase
    {
        public HisBedRoomTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_ROOM>();
        }

        private BridgeDAO<HIS_BED_ROOM> bridgeDAO;

        public bool Truncate(HIS_BED_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BED_ROOM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
