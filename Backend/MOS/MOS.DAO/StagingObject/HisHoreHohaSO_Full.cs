using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisHoreHohaSO : StagingObjectBase
    {
        public HisHoreHohaSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_HORE_HOHA, bool>>> listHisHoreHohaExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HORE_HOHA, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_HORE_HOHA, bool>>> listVHisHoreHohaExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_HORE_HOHA, bool>>>();
    }
}
