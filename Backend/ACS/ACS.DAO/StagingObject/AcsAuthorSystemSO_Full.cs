using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsAuthorSystemSO : StagingObjectBase
    {
        public AcsAuthorSystemSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_AUTHOR_SYSTEM, bool>>> listAcsAuthorSystemExpression = new List<System.Linq.Expressions.Expression<Func<ACS_AUTHOR_SYSTEM, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_ACS_AUTHOR_SYSTEM, bool>>> listVAcsAuthorSystemExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_AUTHOR_SYSTEM, bool>>>();
    }
}
