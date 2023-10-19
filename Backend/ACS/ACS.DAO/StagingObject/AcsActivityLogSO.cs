using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsActivityLogSO : StagingObjectBase
    {
        public AcsActivityLogSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_ACTIVITY_LOG, bool>>> listAcsActivityLogExpression = new List<System.Linq.Expressions.Expression<Func<ACS_ACTIVITY_LOG, bool>>>();
    }
}
