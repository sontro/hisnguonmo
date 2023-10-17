using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaHideControlSO : StagingObjectBase
    {
        public SdaHideControlSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_HIDE_CONTROL, bool>>> listSdaHideControlExpression = new List<System.Linq.Expressions.Expression<Func<SDA_HIDE_CONTROL, bool>>>();
    }
}
