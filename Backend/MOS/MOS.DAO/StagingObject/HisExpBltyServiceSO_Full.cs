using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisExpBltyServiceSO : StagingObjectBase
    {
        public HisExpBltyServiceSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EXP_BLTY_SERVICE, bool>>> listHisExpBltyServiceExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EXP_BLTY_SERVICE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_BLTY_SERVICE, bool>>> listVHisExpBltyServiceExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_BLTY_SERVICE, bool>>>();
    }
}
