using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentBedRoom
{
    partial class HisTreatmentBedRoomUpdate : EntityBase
    {
        public HisTreatmentBedRoomUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_BED_ROOM>();
        }

        private BridgeDAO<HIS_TREATMENT_BED_ROOM> bridgeDAO;

        public bool Update(HIS_TREATMENT_BED_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TREATMENT_BED_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
