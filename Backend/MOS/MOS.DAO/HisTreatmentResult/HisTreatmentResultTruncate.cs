using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentResult
{
    partial class HisTreatmentResultTruncate : EntityBase
    {
        public HisTreatmentResultTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_RESULT>();
        }

        private BridgeDAO<HIS_TREATMENT_RESULT> bridgeDAO;

        public bool Truncate(HIS_TREATMENT_RESULT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TREATMENT_RESULT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
