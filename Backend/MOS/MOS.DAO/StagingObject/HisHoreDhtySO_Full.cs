using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisHoreDhtySO : StagingObjectBase
    {
        public HisHoreDhtySO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_HORE_DHTY, bool>>> listHisHoreDhtyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HORE_DHTY, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_HORE_DHTY, bool>>> listVHisHoreDhtyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_HORE_DHTY, bool>>>();
    }
}
