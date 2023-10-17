using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaDeleteDataSO : StagingObjectBase
    {
        public SdaDeleteDataSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_DELETE_DATA, bool>>> listSdaDeleteDataExpression = new List<System.Linq.Expressions.Expression<Func<SDA_DELETE_DATA, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SDA_DELETE_DATA, bool>>> listVSdaDeleteDataExpression = new List<System.Linq.Expressions.Expression<Func<V_SDA_DELETE_DATA, bool>>>();
    }
}
