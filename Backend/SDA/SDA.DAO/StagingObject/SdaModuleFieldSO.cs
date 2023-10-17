using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaModuleFieldSO : StagingObjectBase
    {
        public SdaModuleFieldSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_MODULE_FIELD, bool>>> listSdaModuleFieldExpression = new List<System.Linq.Expressions.Expression<Func<SDA_MODULE_FIELD, bool>>>();
    }
}
