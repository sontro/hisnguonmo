using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsTokenSO : StagingObjectBase
    {
        public AcsTokenSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_TOKEN, bool>>> listAcsTokenExpression = new List<System.Linq.Expressions.Expression<Func<ACS_TOKEN, bool>>>();
    }
}
