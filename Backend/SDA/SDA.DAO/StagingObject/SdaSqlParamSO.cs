using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaSqlParamSO : StagingObjectBase
    {
        public SdaSqlParamSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_SQL_PARAM, bool>>> listSdaSqlParamExpression = new List<System.Linq.Expressions.Expression<Func<SDA_SQL_PARAM, bool>>>();
    }
}
