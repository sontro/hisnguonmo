using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisRegimenHivSO : StagingObjectBase
    {
        public HisRegimenHivSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_REGIMEN_HIV, bool>>> listHisRegimenHivExpression = new List<System.Linq.Expressions.Expression<Func<HIS_REGIMEN_HIV, bool>>>();
    }
}
