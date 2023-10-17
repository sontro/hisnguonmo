using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDispenseSO : StagingObjectBase
    {
        public HisDispenseSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DISPENSE, bool>>> listHisDispenseExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DISPENSE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_DISPENSE, bool>>> listVHisDispenseExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DISPENSE, bool>>>();
    }
}
