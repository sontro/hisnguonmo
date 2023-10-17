using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisBltyServiceSO : StagingObjectBase
    {
        public HisBltyServiceSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_BLTY_SERVICE, bool>>> listHisBltyServiceExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BLTY_SERVICE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_BLTY_SERVICE, bool>>> listVHisBltyServiceExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BLTY_SERVICE, bool>>>();
    }
}
