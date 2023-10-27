using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsControlRoleSO : StagingObjectBase
    {
        public AcsControlRoleSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_CONTROL_ROLE, bool>>> listAcsControlRoleExpression = new List<System.Linq.Expressions.Expression<Func<ACS_CONTROL_ROLE, bool>>>();
    }
}
