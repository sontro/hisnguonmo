using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicalAssessment
{
    partial class HisMedicalAssessmentUpdate : EntityBase
    {
        public HisMedicalAssessmentUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICAL_ASSESSMENT>();
        }

        private BridgeDAO<HIS_MEDICAL_ASSESSMENT> bridgeDAO;

        public bool Update(HIS_MEDICAL_ASSESSMENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICAL_ASSESSMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
