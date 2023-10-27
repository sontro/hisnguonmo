using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsModuleGroupSO : StagingObjectBase
    {
        public AcsModuleGroupSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_MODULE_GROUP, bool>>> listAcsModuleGroupExpression = new List<System.Linq.Expressions.Expression<Func<ACS_MODULE_GROUP, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_ACS_MODULE_GROUP, bool>>> listVAcsModuleGroupExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_MODULE_GROUP, bool>>>();
    }
}
