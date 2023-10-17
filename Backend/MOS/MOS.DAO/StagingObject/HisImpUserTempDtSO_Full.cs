using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisImpUserTempDtSO : StagingObjectBase
    {
        public HisImpUserTempDtSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_IMP_USER_TEMP_DT, bool>>> listHisImpUserTempDtExpression = new List<System.Linq.Expressions.Expression<Func<HIS_IMP_USER_TEMP_DT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_USER_TEMP_DT, bool>>> listVHisImpUserTempDtExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_USER_TEMP_DT, bool>>>();
    }
}
