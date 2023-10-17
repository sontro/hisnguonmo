using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisImpUserTempSO : StagingObjectBase
    {
        public HisImpUserTempSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_IMP_USER_TEMP, bool>>> listHisImpUserTempExpression = new List<System.Linq.Expressions.Expression<Func<HIS_IMP_USER_TEMP, bool>>>();
    }
}
