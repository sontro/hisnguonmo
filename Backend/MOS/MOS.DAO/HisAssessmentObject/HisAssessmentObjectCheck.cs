using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAssessmentObject
{
    partial class HisAssessmentObjectCheck : EntityBase
    {
        public HisAssessmentObjectCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ASSESSMENT_OBJECT>();
        }

        private BridgeDAO<HIS_ASSESSMENT_OBJECT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
