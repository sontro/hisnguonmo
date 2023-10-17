using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAtcSO : StagingObjectBase
    {
        public HisAtcSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ATC, bool>>> listHisAtcExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ATC, bool>>>();
    }
}
