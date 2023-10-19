using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsModuleSO : StagingObjectBase
    {
        public AcsModuleSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_MODULE, bool>>> listAcsModuleExpression = new List<System.Linq.Expressions.Expression<Func<ACS_MODULE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_ACS_MODULE, bool>>> listVAcsModuleExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_MODULE, bool>>>();
    }
}
