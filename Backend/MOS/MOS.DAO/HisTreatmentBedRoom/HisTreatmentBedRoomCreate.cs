using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentBedRoom
{
    partial class HisTreatmentBedRoomCreate : EntityBase
    {
        public HisTreatmentBedRoomCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_BED_ROOM>();
        }

        private BridgeDAO<HIS_TREATMENT_BED_ROOM> bridgeDAO;

        public bool Create(HIS_TREATMENT_BED_ROOM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TREATMENT_BED_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
