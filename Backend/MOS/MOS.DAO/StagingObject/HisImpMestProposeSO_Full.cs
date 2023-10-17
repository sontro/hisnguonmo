using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisImpMestProposeSO : StagingObjectBase
    {
        public HisImpMestProposeSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_IMP_MEST_PROPOSE, bool>>> listHisImpMestProposeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_IMP_MEST_PROPOSE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_MEST_PROPOSE, bool>>> listVHisImpMestProposeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_MEST_PROPOSE, bool>>>();
    }
}
