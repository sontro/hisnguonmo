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
            listAcsRoleBaseExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
            listVAcsRoleBaseExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_ROLE_BASE, bool>>> listAcsRoleBaseExpression = new List<System.Linq.Expressions.Expression<Func<ACS_ROLE_BASE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_ACS_ROLE_BASE, bool>>> listVAcsRoleBaseExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_ROLE_BASE, bool>>>();
    }
}
