using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisRationSumSttSO : StagingObjectBase
    {
        public HisRationSumSttSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_RATION_SUM_STT, bool>>> listHisRationSumSttExpression = new List<System.Linq.Expressions.Expression<Func<HIS_RATION_SUM_STT, bool>>>();
    }
}
