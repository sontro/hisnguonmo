using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMetyMatySO : StagingObjectBase
    {
        public HisMetyMatySO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_METY_MATY, bool>>> listHisMetyMatyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_METY_MATY, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_METY_MATY, bool>>> listVHisMetyMatyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_METY_MATY, bool>>>();
    }
}
