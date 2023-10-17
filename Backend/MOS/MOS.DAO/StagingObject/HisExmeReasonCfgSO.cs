using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisExmeReasonCfgSO : StagingObjectBase
    {
        public HisExmeReasonCfgSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EXME_REASON_CFG, bool>>> listHisExmeReasonCfgExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EXME_REASON_CFG, bool>>>();
    }
}
