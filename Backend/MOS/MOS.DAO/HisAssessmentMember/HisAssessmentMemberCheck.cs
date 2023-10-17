using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAssessmentMember
{
    partial class HisAssessmentMemberCheck : EntityBase
    {
        public HisAssessmentMemberCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ASSESSMENT_MEMBER>();
        }

        private BridgeDAO<HIS_ASSESSMENT_MEMBER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
