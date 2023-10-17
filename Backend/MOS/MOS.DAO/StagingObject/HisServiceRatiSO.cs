using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisServiceRatiSO : StagingObjectBase
    {
        public HisServiceRatiSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_RATI, bool>>> listHisServiceRatiExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_RATI, bool>>>();
    }
}
