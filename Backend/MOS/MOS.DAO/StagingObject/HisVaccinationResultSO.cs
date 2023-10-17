using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaccinationResultSO : StagingObjectBase
    {
        public HisVaccinationResultSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_RESULT, bool>>> listHisVaccinationResultExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_RESULT, bool>>>();
    }
}
