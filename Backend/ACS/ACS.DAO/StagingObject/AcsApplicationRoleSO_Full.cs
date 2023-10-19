using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsApplicationRoleSO : StagingObjectBase
    {
        public AcsApplicationRoleSO()
        {
            listAcsApplicationRoleExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
            listVAcsApplicationRoleExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_APPLICATION_ROLE, bool>>> listAcsApplicationRoleExpression = new List<System.Linq.Expressions.Expression<Func<ACS_APPLICATION_ROLE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_ACS_APPLICATION_ROLE, bool>>> listVAcsApplicationRoleExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_APPLICATION_ROLE, bool>>>();
    }
}
