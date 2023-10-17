using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEquipmentSet
{
    partial class HisEquipmentSetTruncate : EntityBase
    {
        public HisEquipmentSetTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EQUIPMENT_SET>();
        }

        private BridgeDAO<HIS_EQUIPMENT_SET> bridgeDAO;

        public bool Truncate(HIS_EQUIPMENT_SET data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EQUIPMENT_SET> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
