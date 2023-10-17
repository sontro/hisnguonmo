using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDrugInterventionSO : StagingObjectBase
    {
        public HisDrugInterventionSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DRUG_INTERVENTION, bool>>> listHisDrugInterventionExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DRUG_INTERVENTION, bool>>>();
    }
}
