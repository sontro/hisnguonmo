using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaConfigAppSO : StagingObjectBase
    {
        public SdaConfigAppSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_CONFIG_APP, bool>>> listSdaConfigAppExpression = new List<System.Linq.Expressions.Expression<Func<SDA_CONFIG_APP, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SDA_CONFIG_APP, bool>>> listVSdaConfigAppExpression = new List<System.Linq.Expressions.Expression<Func<V_SDA_CONFIG_APP, bool>>>();
    }
}
