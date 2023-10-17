using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentLogging
{
    partial class HisTreatmentLoggingTruncate : EntityBase
    {
        public HisTreatmentLoggingTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_LOGGING>();
        }

        private BridgeDAO<HIS_TREATMENT_LOGGING> bridgeDAO;

        public bool Truncate(HIS_TREATMENT_LOGGING data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TREATMENT_LOGGING> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
