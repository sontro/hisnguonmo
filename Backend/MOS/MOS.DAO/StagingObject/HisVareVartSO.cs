using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVareVartSO : StagingObjectBase
    {
        public HisVareVartSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VARE_VART, bool>>> listHisVareVartExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VARE_VART, bool>>>();
    }
}
