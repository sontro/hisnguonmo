using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaexVaerSO : StagingObjectBase
    {
        public HisVaexVaerSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VAEX_VAER, bool>>> listHisVaexVaerExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VAEX_VAER, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_VAEX_VAER, bool>>> listVHisVaexVaerExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_VAEX_VAER, bool>>>();
    }
}
