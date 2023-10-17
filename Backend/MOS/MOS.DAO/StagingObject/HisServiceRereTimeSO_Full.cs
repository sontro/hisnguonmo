using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisServiceRereTimeSO : StagingObjectBase
    {
        public HisServiceRereTimeSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_RERE_TIME, bool>>> listHisServiceRereTimeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_RERE_TIME, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_RERE_TIME, bool>>> listVHisServiceRereTimeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_RERE_TIME, bool>>>();
    }
}
