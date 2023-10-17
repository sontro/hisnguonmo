using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEquipmentSet
{
    partial class HisEquipmentSetUpdate : EntityBase
    {
        public HisEquipmentSetUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EQUIPMENT_SET>();
        }

        private BridgeDAO<HIS_EQUIPMENT_SET> bridgeDAO;

        public bool Update(HIS_EQUIPMENT_SET data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EQUIPMENT_SET> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
