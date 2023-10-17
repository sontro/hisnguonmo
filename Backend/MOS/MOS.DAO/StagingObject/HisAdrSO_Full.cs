using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAdrSO : StagingObjectBase
    {
        public HisAdrSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ADR, bool>>> listHisAdrExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ADR, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_ADR, bool>>> listVHisAdrExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ADR, bool>>>();
    }
}
