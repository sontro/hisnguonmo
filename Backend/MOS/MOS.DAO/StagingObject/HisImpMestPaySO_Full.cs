using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisImpMestPaySO : StagingObjectBase
    {
        public HisImpMestPaySO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_IMP_MEST_PAY, bool>>> listHisImpMestPayExpression = new List<System.Linq.Expressions.Expression<Func<HIS_IMP_MEST_PAY, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_MEST_PAY, bool>>> listVHisImpMestPayExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_MEST_PAY, bool>>>();
    }
}
