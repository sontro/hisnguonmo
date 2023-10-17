using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisKskAccessSO : StagingObjectBase
    {
        public HisKskAccessSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_KSK_ACCESS, bool>>> listHisKskAccessExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_ACCESS, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_ACCESS, bool>>> listVHisKskAccessExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_ACCESS, bool>>>();
    }
}
