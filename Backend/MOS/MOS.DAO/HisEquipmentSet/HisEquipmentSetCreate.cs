using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEquipmentSet
{
    partial class HisEquipmentSetCreate : EntityBase
    {
        public HisEquipmentSetCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EQUIPMENT_SET>();
        }

        private BridgeDAO<HIS_EQUIPMENT_SET> bridgeDAO;

        public bool Create(HIS_EQUIPMENT_SET data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EQUIPMENT_SET> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
