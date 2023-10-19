using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsModuleRoleSO : StagingObjectBase
    {
        public AcsModuleRoleSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_MODULE_ROLE, bool>>> listAcsModuleRoleExpression = new List<System.Linq.Expressions.Expression<Func<ACS_MODULE_ROLE, bool>>>();
    }
}
