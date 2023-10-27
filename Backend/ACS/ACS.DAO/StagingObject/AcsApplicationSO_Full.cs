using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsApplicationSO : StagingObjectBase
    {
        public AcsApplicationSO()
        {
            listAcsApplicationExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
            listVAcsApplicationExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_APPLICATION, bool>>> listAcsApplicationExpression = new List<System.Linq.Expressions.Expression<Func<ACS_APPLICATION, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_ACS_APPLICATION, bool>>> listVAcsApplicationExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_APPLICATION, bool>>>();
    }
}
