using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEquipmentSetMaty
{
    partial class HisEquipmentSetMatyUpdate : EntityBase
    {
        public HisEquipmentSetMatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EQUIPMENT_SET_MATY>();
        }

        private BridgeDAO<HIS_EQUIPMENT_SET_MATY> bridgeDAO;

        public bool Update(HIS_EQUIPMENT_SET_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EQUIPMENT_SET_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
