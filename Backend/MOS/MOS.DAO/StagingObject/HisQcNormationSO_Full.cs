using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisQcNormationSO : StagingObjectBase
    {
        public HisQcNormationSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_QC_NORMATION, bool>>> listHisQcNormationExpression = new List<System.Linq.Expressions.Expression<Func<HIS_QC_NORMATION, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_QC_NORMATION, bool>>> listVHisQcNormationExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_QC_NORMATION, bool>>>();
    }
}
