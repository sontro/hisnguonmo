using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPtttTableSO : StagingObjectBase
    {
        public HisPtttTableSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PTTT_TABLE, bool>>> listHisPtttTableExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PTTT_TABLE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_PTTT_TABLE, bool>>> listVHisPtttTableExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PTTT_TABLE, bool>>>();
    }
}
