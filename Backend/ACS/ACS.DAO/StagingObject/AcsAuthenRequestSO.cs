using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsAuthenRequestSO : StagingObjectBase
    {
        public AcsAuthenRequestSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_AUTHEN_REQUEST, bool>>> listAcsAuthenRequestExpression = new List<System.Linq.Expressions.Expression<Func<ACS_AUTHEN_REQUEST, bool>>>();
    }
}
