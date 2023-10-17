using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisUserGroupTempSO : StagingObjectBase
    {
        public HisUserGroupTempSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_USER_GROUP_TEMP, bool>>> listHisUserGroupTempExpression = new List<System.Linq.Expressions.Expression<Func<HIS_USER_GROUP_TEMP, bool>>>();
    }
}
