using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMestMatyDepaSO : StagingObjectBase
    {
        public HisMestMatyDepaSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEST_MATY_DEPA, bool>>> listHisMestMatyDepaExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEST_MATY_DEPA, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_MATY_DEPA, bool>>> listVHisMestMatyDepaExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_MATY_DEPA, bool>>>();
    }
}
