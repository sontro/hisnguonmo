using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentBedRoom
{
    partial class HisTreatmentBedRoomTruncate : EntityBase
    {
        public HisTreatmentBedRoomTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_BED_ROOM>();
        }

        private BridgeDAO<HIS_TREATMENT_BED_ROOM> bridgeDAO;

        public bool Truncate(HIS_TREATMENT_BED_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TREATMENT_BED_ROOM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
