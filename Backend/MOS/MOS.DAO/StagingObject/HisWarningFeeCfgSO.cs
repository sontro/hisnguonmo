using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisWarningFeeCfgSO : StagingObjectBase
    {
        public HisWarningFeeCfgSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_WARNING_FEE_CFG, bool>>> listHisWarningFeeCfgExpression = new List<System.Linq.Expressions.Expression<Func<HIS_WARNING_FEE_CFG, bool>>>();
    }
}
