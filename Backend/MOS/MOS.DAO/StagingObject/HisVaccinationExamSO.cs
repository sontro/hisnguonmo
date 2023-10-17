using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaccinationExamSO : StagingObjectBase
    {
        public HisVaccinationExamSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_EXAM, bool>>> listHisVaccinationExamExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_EXAM, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINATION_EXAM, bool>>> listVHisVaccinationExamExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINATION_EXAM, bool>>>();
    }
}
