using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisQcTypeSO : StagingObjectBase
    {
        public HisQcTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_QC_TYPE, bool>>> listHisQcTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_QC_TYPE, bool>>>();
    }
}
