using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentRoom
{
    partial class HisTreatmentRoomTruncate : EntityBase
    {
        public HisTreatmentRoomTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_ROOM>();
        }

        private BridgeDAO<HIS_TREATMENT_ROOM> bridgeDAO;

        public bool Truncate(HIS_TREATMENT_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TREATMENT_ROOM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
