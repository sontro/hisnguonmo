using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicalAssessment
{
    partial class HisMedicalAssessmentCheck : EntityBase
    {
        public HisMedicalAssessmentCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICAL_ASSESSMENT>();
        }

        private BridgeDAO<HIS_MEDICAL_ASSESSMENT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
