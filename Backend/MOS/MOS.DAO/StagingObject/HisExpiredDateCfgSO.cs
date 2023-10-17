using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisExpiredDateCfgSO : StagingObjectBase
    {
        public HisExpiredDateCfgSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EXPIRED_DATE_CFG, bool>>> listHisExpiredDateCfgExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EXPIRED_DATE_CFG, bool>>>();
    }
}
