using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTreatmentFileSO : StagingObjectBase
    {
        public HisTreatmentFileSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_FILE, bool>>> listHisTreatmentFileExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_FILE, bool>>>();
    }
}
