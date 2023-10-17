using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaccinationSttSO : StagingObjectBase
    {
        public HisVaccinationSttSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_STT, bool>>> listHisVaccinationSttExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_STT, bool>>>();
    }
}
