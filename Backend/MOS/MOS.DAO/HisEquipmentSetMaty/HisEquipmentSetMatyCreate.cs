using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEquipmentSetMaty
{
    partial class HisEquipmentSetMatyCreate : EntityBase
    {
        public HisEquipmentSetMatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EQUIPMENT_SET_MATY>();
        }

        private BridgeDAO<HIS_EQUIPMENT_SET_MATY> bridgeDAO;

        public bool Create(HIS_EQUIPMENT_SET_MATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EQUIPMENT_SET_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
