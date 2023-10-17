using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisBodyPartSO : StagingObjectBase
    {
        public HisBodyPartSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_BODY_PART, bool>>> listHisBodyPartExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BODY_PART, bool>>>();
    }
}
