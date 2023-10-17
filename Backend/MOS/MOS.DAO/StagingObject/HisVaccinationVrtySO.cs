using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaccinationVrtySO : StagingObjectBase
    {
        public HisVaccinationVrtySO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_VRTY, bool>>> listHisVaccinationVrtyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_VRTY, bool>>>();
    }
}
