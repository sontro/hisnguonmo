using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAgeTypeSO : StagingObjectBase
    {
        public HisAgeTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_AGE_TYPE, bool>>> listHisAgeTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_AGE_TYPE, bool>>>();
    }
}
