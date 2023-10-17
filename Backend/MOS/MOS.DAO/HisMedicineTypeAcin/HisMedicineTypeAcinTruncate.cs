using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineTypeAcin
{
    partial class HisMedicineTypeAcinTruncate : EntityBase
    {
        public HisMedicineTypeAcinTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_TYPE_ACIN>();
        }

        private BridgeDAO<HIS_MEDICINE_TYPE_ACIN> bridgeDAO;

        public bool Truncate(HIS_MEDICINE_TYPE_ACIN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDICINE_TYPE_ACIN> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
