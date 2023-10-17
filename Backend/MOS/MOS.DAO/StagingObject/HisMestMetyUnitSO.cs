using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMestMetyUnitSO : StagingObjectBase
    {
        public HisMestMetyUnitSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEST_METY_UNIT, bool>>> listHisMestMetyUnitExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEST_METY_UNIT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_METY_UNIT, bool>>> listVHisMestMetyUnitExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_METY_UNIT, bool>>>();
    }
}
