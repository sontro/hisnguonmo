using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPrepareSO : StagingObjectBase
    {
        public HisPrepareSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PREPARE, bool>>> listHisPrepareExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PREPARE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_PREPARE, bool>>> listVHisPrepareExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PREPARE, bool>>>();
    }
}
