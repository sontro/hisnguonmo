using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMetyMetySO : StagingObjectBase
    {
        public HisMetyMetySO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_METY_METY, bool>>> listHisMetyMetyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_METY_METY, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_METY_METY, bool>>> listVHisMetyMetyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_METY_METY, bool>>>();
    }
}
