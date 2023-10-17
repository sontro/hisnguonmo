using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDiimTypeSO : StagingObjectBase
    {
        public HisDiimTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DIIM_TYPE, bool>>> listHisDiimTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DIIM_TYPE, bool>>>();
    }
}
