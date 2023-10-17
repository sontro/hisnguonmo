using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDiseaseTypeSO : StagingObjectBase
    {
        public HisDiseaseTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DISEASE_TYPE, bool>>> listHisDiseaseTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DISEASE_TYPE, bool>>>();
    }
}
