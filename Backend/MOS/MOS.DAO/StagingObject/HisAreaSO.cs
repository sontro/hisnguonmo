using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAreaSO : StagingObjectBase
    {
        public HisAreaSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_AREA, bool>>> listHisAreaExpression = new List<System.Linq.Expressions.Expression<Func<HIS_AREA, bool>>>();
    }
}
