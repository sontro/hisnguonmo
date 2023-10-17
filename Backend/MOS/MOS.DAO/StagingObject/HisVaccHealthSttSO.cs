using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaccHealthSttSO : StagingObjectBase
    {
        public HisVaccHealthSttSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VACC_HEALTH_STT, bool>>> listHisVaccHealthSttExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACC_HEALTH_STT, bool>>>();
    }
}
