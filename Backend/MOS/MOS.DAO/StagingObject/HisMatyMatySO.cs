using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMatyMatySO : StagingObjectBase
    {
        public HisMatyMatySO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MATY_MATY, bool>>> listHisMatyMatyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MATY_MATY, bool>>>();
    }
}
