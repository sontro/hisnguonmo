using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisEmployeeSO : StagingObjectBase
    {
        public HisEmployeeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EMPLOYEE, bool>>> listHisEmployeeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EMPLOYEE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_EMPLOYEE, bool>>> listVHisEmployeeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EMPLOYEE, bool>>>();
    }
}
