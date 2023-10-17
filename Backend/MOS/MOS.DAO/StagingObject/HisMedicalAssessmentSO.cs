using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMedicalAssessmentSO : StagingObjectBase
    {
        public HisMedicalAssessmentSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEDICAL_ASSESSMENT, bool>>> listHisMedicalAssessmentExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICAL_ASSESSMENT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICAL_ASSESSMENT, bool>>> listVHisMedicalAssessmentExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICAL_ASSESSMENT, bool>>>();
    }
}
