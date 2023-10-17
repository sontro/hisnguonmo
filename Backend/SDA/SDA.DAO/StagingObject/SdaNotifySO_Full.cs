using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaNotifySO : StagingObjectBase
    {
        public SdaNotifySO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_NOTIFY, bool>>> listSdaNotifyExpression = new List<System.Linq.Expressions.Expression<Func<SDA_NOTIFY, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SDA_NOTIFY, bool>>> listVSdaNotifyExpression = new List<System.Linq.Expressions.Expression<Func<V_SDA_NOTIFY, bool>>>();
    }
}
