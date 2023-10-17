using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMedicineServiceSO : StagingObjectBase
    {
        public HisMedicineServiceSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_SERVICE, bool>>> listHisMedicineServiceExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_SERVICE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_SERVICE, bool>>> listVHisMedicineServiceExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_SERVICE, bool>>>();
    }
}
