using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaccinationReactSO : StagingObjectBase
    {
        public HisVaccinationReactSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_REACT, bool>>> listHisVaccinationReactExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_REACT, bool>>>();
    }
}
