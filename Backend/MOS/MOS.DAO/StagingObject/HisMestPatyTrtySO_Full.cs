using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMestPatyTrtySO : StagingObjectBase
    {
        public HisMestPatyTrtySO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEST_PATY_TRTY, bool>>> listHisMestPatyTrtyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEST_PATY_TRTY, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_PATY_TRTY, bool>>> listVHisMestPatyTrtyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_PATY_TRTY, bool>>>();
    }
}
