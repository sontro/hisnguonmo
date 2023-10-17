using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAntibioticNewRegSO : StagingObjectBase
    {
        public HisAntibioticNewRegSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_NEW_REG, bool>>> listHisAntibioticNewRegExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_NEW_REG, bool>>>();
    }
}
