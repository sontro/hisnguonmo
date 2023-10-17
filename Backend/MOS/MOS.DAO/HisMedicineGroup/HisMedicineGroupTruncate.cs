using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineGroup
{
    partial class HisMedicineGroupTruncate : EntityBase
    {
        public HisMedicineGroupTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_GROUP>();
        }

        private BridgeDAO<HIS_MEDICINE_GROUP> bridgeDAO;

        public bool Truncate(HIS_MEDICINE_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDICINE_GROUP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
