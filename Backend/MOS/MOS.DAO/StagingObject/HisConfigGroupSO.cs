using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisConfigGroupSO : StagingObjectBase
    {
        public HisConfigGroupSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_CONFIG_GROUP, bool>>> listHisConfigGroupExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CONFIG_GROUP, bool>>>();
    }
}
