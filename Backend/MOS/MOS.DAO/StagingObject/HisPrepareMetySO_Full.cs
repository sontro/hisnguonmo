using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPrepareMetySO : StagingObjectBase
    {
        public HisPrepareMetySO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PREPARE_METY, bool>>> listHisPrepareMetyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PREPARE_METY, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_PREPARE_METY, bool>>> listVHisPrepareMetyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PREPARE_METY, bool>>>();
    }
}
