using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisDocHoldTypeSO : StagingObjectBase
    {
        public HisDocHoldTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_DOC_HOLD_TYPE, bool>>> listHisDocHoldTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DOC_HOLD_TYPE, bool>>>();
    }
}
