using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAlertSO : StagingObjectBase
    {
        public HisAlertSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ALERT, bool>>> listHisAlertExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ALERT, bool>>>();
    }
}
