using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentUnlimit
{
    partial class HisTreatmentUnlimitTruncate : EntityBase
    {
        public HisTreatmentUnlimitTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_UNLIMIT>();
        }

        private BridgeDAO<HIS_TREATMENT_UNLIMIT> bridgeDAO;

        public bool Truncate(HIS_TREATMENT_UNLIMIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TREATMENT_UNLIMIT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
