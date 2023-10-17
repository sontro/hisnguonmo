using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisContraindicationSO : StagingObjectBase
    {
        public HisContraindicationSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_CONTRAINDICATION, bool>>> listHisContraindicationExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CONTRAINDICATION, bool>>>();
    }
}
