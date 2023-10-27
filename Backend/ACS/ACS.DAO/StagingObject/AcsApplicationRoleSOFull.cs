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

        }

        public List<System.Linq.Expressions.Expression<Func<ACS_APPLICATION_ROLE, bool>>> listEvAcsApplicationRoleDTOExpression = new List<System.Linq.Expressions.Expression<Func<ACS_APPLICATION_ROLE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_ACS_APPLICATION_ROLE, bool>>> listV_ACS_APPLICATION_ROLEExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_APPLICATION_ROLE, bool>>>();
    }
}
