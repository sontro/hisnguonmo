using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAssessmentMember
{
    partial class HisAssessmentMemberUpdate : EntityBase
    {
        public HisAssessmentMemberUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ASSESSMENT_MEMBER>();
        }

        private BridgeDAO<HIS_ASSESSMENT_MEMBER> bridgeDAO;

        public bool Update(HIS_ASSESSMENT_MEMBER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ASSESSMENT_MEMBER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
