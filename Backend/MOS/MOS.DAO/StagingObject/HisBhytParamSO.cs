using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisBhytParamSO : StagingObjectBase
    {
        public HisBhytParamSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_BHYT_PARAM, bool>>> listHisBhytParamExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BHYT_PARAM, bool>>>();
    }
}
