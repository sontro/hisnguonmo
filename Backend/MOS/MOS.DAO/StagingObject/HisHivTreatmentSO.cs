using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisHivTreatmentSO : StagingObjectBase
    {
        public HisHivTreatmentSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_HIV_TREATMENT, bool>>> listHisHivTreatmentExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HIV_TREATMENT, bool>>>();
    }
}
