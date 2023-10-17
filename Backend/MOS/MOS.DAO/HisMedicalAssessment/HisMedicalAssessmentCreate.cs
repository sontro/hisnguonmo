using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicalAssessment
{
    partial class HisMedicalAssessmentCreate : EntityBase
    {
        public HisMedicalAssessmentCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICAL_ASSESSMENT>();
        }

        private BridgeDAO<HIS_MEDICAL_ASSESSMENT> bridgeDAO;

        public bool Create(HIS_MEDICAL_ASSESSMENT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICAL_ASSESSMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
