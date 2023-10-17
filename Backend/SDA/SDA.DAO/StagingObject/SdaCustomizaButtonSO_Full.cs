using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaCustomizaButtonSO : StagingObjectBase
    {
        public SdaCustomizaButtonSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_CUSTOMIZA_BUTTON, bool>>> listSdaCustomizaButtonExpression = new List<System.Linq.Expressions.Expression<Func<SDA_CUSTOMIZA_BUTTON, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SDA_CUSTOMIZA_BUTTON, bool>>> listVSdaCustomizaButtonExpression = new List<System.Linq.Expressions.Expression<Func<V_SDA_CUSTOMIZA_BUTTON, bool>>>();
    }
}
