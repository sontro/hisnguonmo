using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisKskOtherSO : StagingObjectBase
    {
        public HisKskOtherSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_KSK_OTHER, bool>>> listHisKskOtherExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_OTHER, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_OTHER, bool>>> listVHisKskOtherExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_OTHER, bool>>>();
    }
}
