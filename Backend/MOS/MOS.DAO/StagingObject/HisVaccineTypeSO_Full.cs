using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaccineTypeSO : StagingObjectBase
    {
        public HisVaccineTypeSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VACCINE_TYPE, bool>>> listHisVaccineTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACCINE_TYPE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINE_TYPE, bool>>> listVHisVaccineTypeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINE_TYPE, bool>>>();
    }
}
