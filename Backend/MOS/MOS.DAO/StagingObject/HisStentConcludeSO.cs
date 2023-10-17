using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisStentConcludeSO : StagingObjectBase
    {
        public HisStentConcludeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_STENT_CONCLUDE, bool>>> listHisStentConcludeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_STENT_CONCLUDE, bool>>>();
    }
}
