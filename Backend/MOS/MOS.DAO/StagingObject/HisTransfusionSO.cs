using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTransfusionSO : StagingObjectBase
    {
        public HisTransfusionSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TRANSFUSION, bool>>> listHisTransfusionExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRANSFUSION, bool>>>();
    }
}
