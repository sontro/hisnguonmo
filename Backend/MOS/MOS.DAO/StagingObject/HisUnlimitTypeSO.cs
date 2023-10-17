using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisUnlimitTypeSO : StagingObjectBase
    {
        public HisUnlimitTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_UNLIMIT_TYPE, bool>>> listHisUnlimitTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_UNLIMIT_TYPE, bool>>>();
    }
}
