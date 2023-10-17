using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAntigenMetySO : StagingObjectBase
    {
        public HisAntigenMetySO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ANTIGEN_METY, bool>>> listHisAntigenMetyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ANTIGEN_METY, bool>>>();
    }
}
