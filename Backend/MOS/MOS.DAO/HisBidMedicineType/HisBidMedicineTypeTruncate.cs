using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBidMedicineType
{
    partial class HisBidMedicineTypeTruncate : EntityBase
    {
        public HisBidMedicineTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BID_MEDICINE_TYPE>();
        }

        private BridgeDAO<HIS_BID_MEDICINE_TYPE> bridgeDAO;

        public bool Truncate(HIS_BID_MEDICINE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BID_MEDICINE_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
