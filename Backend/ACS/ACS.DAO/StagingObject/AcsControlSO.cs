using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsControlSO : StagingObjectBase
    {
        public AcsControlSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_CONTROL, bool>>> listAcsControlExpression = new List<System.Linq.Expressions.Expression<Func<ACS_CONTROL, bool>>>();
    }
}
