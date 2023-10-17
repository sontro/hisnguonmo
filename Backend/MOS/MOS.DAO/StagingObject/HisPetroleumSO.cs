using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPetroleumSO : StagingObjectBase
    {
        public HisPetroleumSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PETROLEUM, bool>>> listHisPetroleumExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PETROLEUM, bool>>>();
    }
}
