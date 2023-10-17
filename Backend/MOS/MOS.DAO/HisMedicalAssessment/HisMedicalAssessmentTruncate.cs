using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicalAssessment
{
    partial class HisMedicalAssessmentTruncate : EntityBase
    {
        public HisMedicalAssessmentTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICAL_ASSESSMENT>();
        }

        private BridgeDAO<HIS_MEDICAL_ASSESSMENT> bridgeDAO;

        public bool Truncate(HIS_MEDICAL_ASSESSMENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDICAL_ASSESSMENT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
