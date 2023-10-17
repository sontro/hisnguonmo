using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaLanguageSO : StagingObjectBase
    {
        public SdaLanguageSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_LANGUAGE, bool>>> listSdaLanguageExpression = new List<System.Linq.Expressions.Expression<Func<SDA_LANGUAGE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SDA_LANGUAGE, bool>>> listVSdaLanguageExpression = new List<System.Linq.Expressions.Expression<Func<V_SDA_LANGUAGE, bool>>>();
    }
}
