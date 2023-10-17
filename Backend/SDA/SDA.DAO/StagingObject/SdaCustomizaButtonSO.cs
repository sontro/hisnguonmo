using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaCustomizeButtonSO : StagingObjectBase
    {
        public SdaCustomizeButtonSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_CUSTOMIZE_BUTTON, bool>>> listSdaCustomizeButtonExpression = new List<System.Linq.Expressions.Expression<Func<SDA_CUSTOMIZE_BUTTON, bool>>>();
    }
}
