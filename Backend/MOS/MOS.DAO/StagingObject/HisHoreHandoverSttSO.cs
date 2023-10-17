using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisHoreHandoverSttSO : StagingObjectBase
    {
        public HisHoreHandoverSttSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_HORE_HANDOVER_STT, bool>>> listHisHoreHandoverSttExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HORE_HANDOVER_STT, bool>>>();
    }
}
