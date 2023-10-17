using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPermissionSO : StagingObjectBase
    {
        public HisPermissionSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PERMISSION, bool>>> listHisPermissionExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PERMISSION, bool>>>();
    }
}
