using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisKskServiceSO : StagingObjectBase
    {
        public HisKskServiceSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_KSK_SERVICE, bool>>> listHisKskServiceExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_SERVICE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_SERVICE, bool>>> listVHisKskServiceExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_SERVICE, bool>>>();
    }
}
