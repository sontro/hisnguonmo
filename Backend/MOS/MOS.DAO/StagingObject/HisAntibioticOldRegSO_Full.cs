using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAntibioticOldRegSO : StagingObjectBase
    {
        public HisAntibioticOldRegSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_OLD_REG, bool>>> listHisAntibioticOldRegExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_OLD_REG, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIBIOTIC_OLD_REG, bool>>> listVHisAntibioticOldRegExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIBIOTIC_OLD_REG, bool>>>();
    }
}
