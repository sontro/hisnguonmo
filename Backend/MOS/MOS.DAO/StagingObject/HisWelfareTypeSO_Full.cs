using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisWelfareTypeSO : StagingObjectBase
    {
        public HisWelfareTypeSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_WELFARE_TYPE, bool>>> listHisWelfareTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_WELFARE_TYPE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_WELFARE_TYPE, bool>>> listVHisWelfareTypeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_WELFARE_TYPE, bool>>>();
    }
}
