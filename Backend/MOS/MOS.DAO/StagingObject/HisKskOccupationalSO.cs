using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisKskOccupationalSO : StagingObjectBase
    {
        public HisKskOccupationalSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_KSK_OCCUPATIONAL, bool>>> listHisKskOccupationalExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_OCCUPATIONAL, bool>>>();
    }
}
