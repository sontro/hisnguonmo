using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaccReactPlaceSO : StagingObjectBase
    {
        public HisVaccReactPlaceSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VACC_REACT_PLACE, bool>>> listHisVaccReactPlaceExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACC_REACT_PLACE, bool>>>();
    }
}
