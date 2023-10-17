using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisRejectAlertSO : StagingObjectBase
    {
        public HisRejectAlertSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_REJECT_ALERT, bool>>> listHisRejectAlertExpression = new List<System.Linq.Expressions.Expression<Func<HIS_REJECT_ALERT, bool>>>();
    }
}
