using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisContactSO : StagingObjectBase
    {
        public HisContactSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_CONTACT, bool>>> listHisContactExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CONTACT, bool>>>();
    }
}
