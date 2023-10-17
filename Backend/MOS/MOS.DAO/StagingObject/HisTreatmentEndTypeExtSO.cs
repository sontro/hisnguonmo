using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTreatmentEndTypeExtSO : StagingObjectBase
    {
        public HisTreatmentEndTypeExtSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_END_TYPE_EXT, bool>>> listHisTreatmentEndTypeExtExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_END_TYPE_EXT, bool>>>();
    }
}
