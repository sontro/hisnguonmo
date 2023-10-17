using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisUserGroupTempDtSO : StagingObjectBase
    {
        public HisUserGroupTempDtSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_USER_GROUP_TEMP_DT, bool>>> listHisUserGroupTempDtExpression = new List<System.Linq.Expressions.Expression<Func<HIS_USER_GROUP_TEMP_DT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_USER_GROUP_TEMP_DT, bool>>> listVHisUserGroupTempDtExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_USER_GROUP_TEMP_DT, bool>>>();
    }
}
