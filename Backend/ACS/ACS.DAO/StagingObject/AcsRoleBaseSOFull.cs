using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsRoleBaseSO : StagingObjectBase
    {
        public AcsRoleBaseSO()
        {

        }

        public List<System.Linq.Expressions.Expression<Func<ACS_ROLE_BASE, bool>>> listEvAcsRoleBaseDTOExpression = new List<System.Linq.Expressions.Expression<Func<ACS_ROLE_BASE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_ACS_ROLE_BASE, bool>>> listV_ACS_ROLE_BASEExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_ROLE_BASE, bool>>>();
    }
}
