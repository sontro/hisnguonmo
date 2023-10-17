using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisCareTempSO : StagingObjectBase
    {
        public HisCareTempSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_CARE_TEMP, bool>>> listHisCareTempExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CARE_TEMP, bool>>>();
    }
}
