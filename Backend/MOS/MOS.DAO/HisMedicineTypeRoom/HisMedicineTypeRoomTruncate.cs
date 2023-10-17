using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineTypeRoom
{
    partial class HisMedicineTypeRoomTruncate : EntityBase
    {
        public HisMedicineTypeRoomTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_TYPE_ROOM>();
        }

        private BridgeDAO<HIS_MEDICINE_TYPE_ROOM> bridgeDAO;

        public bool Truncate(HIS_MEDICINE_TYPE_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDICINE_TYPE_ROOM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
