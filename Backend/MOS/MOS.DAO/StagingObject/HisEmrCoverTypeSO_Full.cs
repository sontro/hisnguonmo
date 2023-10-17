using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisEmrCoverTypeSO : StagingObjectBase
    {
        public HisEmrCoverTypeSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EMR_COVER_TYPE, bool>>> listHisEmrCoverTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EMR_COVER_TYPE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_EMR_COVER_TYPE, bool>>> listVHisEmrCoverTypeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EMR_COVER_TYPE, bool>>>();
    }
}
