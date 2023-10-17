using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisCashierAddConfigSO : StagingObjectBase
    {
        public HisCashierAddConfigSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_CASHIER_ADD_CONFIG, bool>>> listHisCashierAddConfigExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CASHIER_ADD_CONFIG, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_CASHIER_ADD_CONFIG, bool>>> listVHisCashierAddConfigExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_CASHIER_ADD_CONFIG, bool>>>();
    }
}
