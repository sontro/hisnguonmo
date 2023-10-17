using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTranPatiTechSO : StagingObjectBase
    {
        public HisTranPatiTechSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TRAN_PATI_TECH, bool>>> listHisTranPatiTechExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRAN_PATI_TECH, bool>>>();
    }
}
