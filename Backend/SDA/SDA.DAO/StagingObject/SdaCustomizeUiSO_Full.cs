using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaCustomizeUiSO : StagingObjectBase
    {
        public SdaCustomizeUiSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_CUSTOMIZE_UI, bool>>> listSdaCustomizeUiExpression = new List<System.Linq.Expressions.Expression<Func<SDA_CUSTOMIZE_UI, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SDA_CUSTOMIZE_UI, bool>>> listVSdaCustomizeUiExpression = new List<System.Linq.Expressions.Expression<Func<V_SDA_CUSTOMIZE_UI, bool>>>();
    }
}
