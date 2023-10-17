using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAssessmentObject
{
    partial class HisAssessmentObjectTruncate : EntityBase
    {
        public HisAssessmentObjectTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ASSESSMENT_OBJECT>();
        }

        private BridgeDAO<HIS_ASSESSMENT_OBJECT> bridgeDAO;

        public bool Truncate(HIS_ASSESSMENT_OBJECT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ASSESSMENT_OBJECT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
