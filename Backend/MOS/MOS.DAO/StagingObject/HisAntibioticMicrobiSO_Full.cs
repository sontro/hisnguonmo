using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAntibioticMicrobiSO : StagingObjectBase
    {
        public HisAntibioticMicrobiSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_MICROBI, bool>>> listHisAntibioticMicrobiExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_MICROBI, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIBIOTIC_MICROBI, bool>>> listVHisAntibioticMicrobiExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIBIOTIC_MICROBI, bool>>>();
    }
}
