using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisNextTreaIntrSO : StagingObjectBase
    {
        public HisNextTreaIntrSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_NEXT_TREA_INTR, bool>>> listHisNextTreaIntrExpression = new List<System.Linq.Expressions.Expression<Func<HIS_NEXT_TREA_INTR, bool>>>();
    }
}
