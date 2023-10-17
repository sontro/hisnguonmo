using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAssessmentMember
{
    partial class HisAssessmentMemberCreate : EntityBase
    {
        public HisAssessmentMemberCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ASSESSMENT_MEMBER>();
        }

        private BridgeDAO<HIS_ASSESSMENT_MEMBER> bridgeDAO;

        public bool Create(HIS_ASSESSMENT_MEMBER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ASSESSMENT_MEMBER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
