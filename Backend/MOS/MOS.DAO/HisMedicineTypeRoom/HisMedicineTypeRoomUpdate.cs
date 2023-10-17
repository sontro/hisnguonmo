using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineTypeRoom
{
    partial class HisMedicineTypeRoomUpdate : EntityBase
    {
        public HisMedicineTypeRoomUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_TYPE_ROOM>();
        }

        private BridgeDAO<HIS_MEDICINE_TYPE_ROOM> bridgeDAO;

        public bool Update(HIS_MEDICINE_TYPE_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICINE_TYPE_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
