using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAdrMedicineTypeSO : StagingObjectBase
    {
        public HisAdrMedicineTypeSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ADR_MEDICINE_TYPE, bool>>> listHisAdrMedicineTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ADR_MEDICINE_TYPE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_ADR_MEDICINE_TYPE, bool>>> listVHisAdrMedicineTypeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ADR_MEDICINE_TYPE, bool>>>();
    }
}
