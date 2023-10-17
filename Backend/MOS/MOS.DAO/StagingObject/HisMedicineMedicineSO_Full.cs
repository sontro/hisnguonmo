using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMedicineMedicineSO : StagingObjectBase
    {
        public HisMedicineMedicineSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_MEDICINE, bool>>> listHisMedicineMedicineExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_MEDICINE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_MEDICINE, bool>>> listVHisMedicineMedicineExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_MEDICINE, bool>>>();
    }
}
