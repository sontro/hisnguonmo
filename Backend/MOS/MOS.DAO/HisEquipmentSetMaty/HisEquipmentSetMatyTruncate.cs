using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEquipmentSetMaty
{
    partial class HisEquipmentSetMatyTruncate : EntityBase
    {
        public HisEquipmentSetMatyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EQUIPMENT_SET_MATY>();
        }

        private BridgeDAO<HIS_EQUIPMENT_SET_MATY> bridgeDAO;

        public bool Truncate(HIS_EQUIPMENT_SET_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EQUIPMENT_SET_MATY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
