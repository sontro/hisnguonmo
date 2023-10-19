using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsRoleAuthorSO : StagingObjectBase
    {
        public AcsRoleAuthorSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_ROLE_AUTHOR, bool>>> listAcsRoleAuthorExpression = new List<System.Linq.Expressions.Expression<Func<ACS_ROLE_AUTHOR, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_ACS_ROLE_AUTHOR, bool>>> listVAcsRoleAuthorExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_ROLE_AUTHOR, bool>>>();
    }
}
