using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAssessmentMember
{
    partial class HisAssessmentMemberTruncate : EntityBase
    {
        public HisAssessmentMemberTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ASSESSMENT_MEMBER>();
        }

        private BridgeDAO<HIS_ASSESSMENT_MEMBER> bridgeDAO;

        public bool Truncate(HIS_ASSESSMENT_MEMBER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ASSESSMENT_MEMBER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
