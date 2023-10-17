using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineTypeRoom
{
    partial class HisMedicineTypeRoomCreate : EntityBase
    {
        public HisMedicineTypeRoomCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_TYPE_ROOM>();
        }

        private BridgeDAO<HIS_MEDICINE_TYPE_ROOM> bridgeDAO;

        public bool Create(HIS_MEDICINE_TYPE_ROOM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICINE_TYPE_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
