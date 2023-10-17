using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaccinationVrplSO : StagingObjectBase
    {
        public HisVaccinationVrplSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_VRPL, bool>>> listHisVaccinationVrplExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_VRPL, bool>>>();
    }
}
