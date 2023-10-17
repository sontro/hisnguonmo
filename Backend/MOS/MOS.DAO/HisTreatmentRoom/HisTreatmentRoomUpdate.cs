using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentRoom
{
    partial class HisTreatmentRoomUpdate : EntityBase
    {
        public HisTreatmentRoomUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_ROOM>();
        }

        private BridgeDAO<HIS_TREATMENT_ROOM> bridgeDAO;

        public bool Update(HIS_TREATMENT_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TREATMENT_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
