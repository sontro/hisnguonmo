using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisBankSO : StagingObjectBase
    {
        public HisBankSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_BANK, bool>>> listHisBankExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BANK, bool>>>();
    }
}
