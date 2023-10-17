using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisFuexTypeSO : StagingObjectBase
    {
        public HisFuexTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_FUEX_TYPE, bool>>> listHisFuexTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_FUEX_TYPE, bool>>>();
    }
}
