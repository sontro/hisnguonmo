using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMedicineTypeSO : StagingObjectBase
    {
        public HisMedicineTypeSO()
        {
            //listHisMedicineTypeExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
            //listVHisMedicineTypeExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_TYPE, bool>>> listHisMedicineTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_TYPE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_TYPE, bool>>> listVHisMedicineTypeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_TYPE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_TYPE_1, bool>>> listVHisMedicineType1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_TYPE_1, bool>>>();
    }
}
