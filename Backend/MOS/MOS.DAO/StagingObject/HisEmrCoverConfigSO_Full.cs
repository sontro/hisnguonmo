using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisEmrCoverConfigSO : StagingObjectBase
    {
        public HisEmrCoverConfigSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EMR_COVER_CONFIG, bool>>> listHisEmrCoverConfigExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EMR_COVER_CONFIG, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_EMR_COVER_CONFIG, bool>>> listVHisEmrCoverConfigExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EMR_COVER_CONFIG, bool>>>();
    }
}
