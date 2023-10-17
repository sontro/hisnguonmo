using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTreatmentUnlimitSO : StagingObjectBase
    {
        public HisTreatmentUnlimitSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_UNLIMIT, bool>>> listHisTreatmentUnlimitExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_UNLIMIT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_UNLIMIT, bool>>> listVHisTreatmentUnlimitExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_UNLIMIT, bool>>>();
    }
}
