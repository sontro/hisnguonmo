using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisProcessingMethodSO : StagingObjectBase
    {
        public HisProcessingMethodSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PROCESSING_METHOD, bool>>> listHisProcessingMethodExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PROCESSING_METHOD, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_PROCESSING_METHOD, bool>>> listVHisProcessingMethodExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PROCESSING_METHOD, bool>>>();
    }
}
