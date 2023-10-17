using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAntibioticRequestSO : StagingObjectBase
    {
        public HisAntibioticRequestSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_REQUEST, bool>>> listHisAntibioticRequestExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_REQUEST, bool>>>();
    }
}
