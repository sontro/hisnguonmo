using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsRoleUserSO : StagingObjectBase
    {
        public AcsRoleUserSO()
        {
            listAcsRoleUserExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
            listVAcsRoleUserExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_ROLE_USER, bool>>> listAcsRoleUserExpression = new List<System.Linq.Expressions.Expression<Func<ACS_ROLE_USER, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_ACS_ROLE_USER, bool>>> listVAcsRoleUserExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_ROLE_USER, bool>>>();
    }
}
