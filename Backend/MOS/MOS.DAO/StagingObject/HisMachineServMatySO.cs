using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMachineServMatySO : StagingObjectBase
    {
        public HisMachineServMatySO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MACHINE_SERV_MATY, bool>>> listHisMachineServMatyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MACHINE_SERV_MATY, bool>>>();
    }
}
