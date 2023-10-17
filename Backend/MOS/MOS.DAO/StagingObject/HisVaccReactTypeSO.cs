using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaccReactTypeSO : StagingObjectBase
    {
        public HisVaccReactTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VACC_REACT_TYPE, bool>>> listHisVaccReactTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACC_REACT_TYPE, bool>>>();
    }
}
