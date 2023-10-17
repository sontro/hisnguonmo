using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMixedMedicineSO : StagingObjectBase
    {
        public HisMixedMedicineSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MIXED_MEDICINE, bool>>> listHisMixedMedicineExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MIXED_MEDICINE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MIXED_MEDICINE, bool>>> listVHisMixedMedicineExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MIXED_MEDICINE, bool>>>();
    }
}
