using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPtttHighTechSO : StagingObjectBase
    {
        public HisPtttHighTechSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PTTT_HIGH_TECH, bool>>> listHisPtttHighTechExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PTTT_HIGH_TECH, bool>>>();
    }
}
