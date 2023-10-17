using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAntigenSO : StagingObjectBase
    {
        public HisAntigenSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ANTIGEN, bool>>> listHisAntigenExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ANTIGEN, bool>>>();
    }
}
