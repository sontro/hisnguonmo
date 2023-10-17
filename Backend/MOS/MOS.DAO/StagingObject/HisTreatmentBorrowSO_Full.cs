using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTreatmentBorrowSO : StagingObjectBase
    {
        public HisTreatmentBorrowSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_BORROW, bool>>> listHisTreatmentBorrowExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_BORROW, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_BORROW, bool>>> listVHisTreatmentBorrowExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_BORROW, bool>>>();
    }
}
