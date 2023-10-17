using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisBloodGiverSO : StagingObjectBase
    {
        public HisBloodGiverSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_BLOOD_GIVER, bool>>> listHisBloodGiverExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BLOOD_GIVER, bool>>>();
    }
}
