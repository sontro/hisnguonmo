using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaTranslateSO : StagingObjectBase
    {
        public SdaTranslateSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_TRANSLATE, bool>>> listSdaTranslateExpression = new List<System.Linq.Expressions.Expression<Func<SDA_TRANSLATE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SDA_TRANSLATE, bool>>> listVSdaTranslateExpression = new List<System.Linq.Expressions.Expression<Func<V_SDA_TRANSLATE, bool>>>();
    }
}
