using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPatientClassifySO : StagingObjectBase
    {
        public HisPatientClassifySO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PATIENT_CLASSIFY, bool>>> listHisPatientClassifyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PATIENT_CLASSIFY, bool>>>();
    }
}
