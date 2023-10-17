using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaSqlSO : StagingObjectBase
    {
        public SdaSqlSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_SQL, bool>>> listSdaSqlExpression = new List<System.Linq.Expressions.Expression<Func<SDA_SQL, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SDA_SQL, bool>>> listVSdaSqlExpression = new List<System.Linq.Expressions.Expression<Func<V_SDA_SQL, bool>>>();
    }
}
