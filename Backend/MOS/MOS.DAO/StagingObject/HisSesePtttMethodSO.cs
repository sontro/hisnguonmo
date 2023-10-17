using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSesePtttMethodSO : StagingObjectBase
    {
        public HisSesePtttMethodSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SESE_PTTT_METHOD, bool>>> listHisSesePtttMethodExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SESE_PTTT_METHOD, bool>>>();
    }
}
