using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPrepareMatySO : StagingObjectBase
    {
        public HisPrepareMatySO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PREPARE_MATY, bool>>> listHisPrepareMatyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PREPARE_MATY, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_PREPARE_MATY, bool>>> listVHisPrepareMatyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PREPARE_MATY, bool>>>();
    }
}
