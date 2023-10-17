using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisInteractiveGradeSO : StagingObjectBase
    {
        public HisInteractiveGradeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_INTERACTIVE_GRADE, bool>>> listHisInteractiveGradeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_INTERACTIVE_GRADE, bool>>>();
    }
}
