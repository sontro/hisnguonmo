using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisServiceHeinSO : StagingObjectBase
    {
        public HisServiceHeinSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_HEIN, bool>>> listHisServiceHeinExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_HEIN, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_HEIN, bool>>> listVHisServiceHeinExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_HEIN, bool>>>();
    }
}
