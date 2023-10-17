using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisCareTempDetailSO : StagingObjectBase
    {
        public HisCareTempDetailSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_CARE_TEMP_DETAIL, bool>>> listHisCareTempDetailExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CARE_TEMP_DETAIL, bool>>>();
    }
}
